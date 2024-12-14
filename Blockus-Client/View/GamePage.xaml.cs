using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.GameBoard;
using Blockus_Client.Blocks;
using System.Windows.Input;
using System.Windows;
using System;
using System.Windows.Controls;
using System.ServiceModel;
using System.Linq;
using System.Collections.Generic;
using Blockus_Client.UserControls;

namespace Blockus_Client.View
{
    public partial class GamePage : Page, IMatchServiceCallback
    {
        private BoardPainter _boardPainter;

        private Dictionary<BlockusService.Color, ActivePlayerCard> _cards = new Dictionary<BlockusService.Color, ActivePlayerCard>();
        private GameState _gameState = new GameState();
        private MatchServiceClient _matchClient;

        private string _matchCode;
        private MatchDTO _match; 
        private bool _isMyTurn = false;
        private BlockusService.Color currentColor; 

        public GamePage()
        {
            InitializeComponent();
            SetUpUI();
            SetUpMatch();
            Frame_Chat.Navigate(new ChatPage(_matchCode));

        }

        private void SetUpUI()
        {
            var tiles = SessionManager.Instance.GetTilesConfiguration();
            var tileImages = TilesImageManager.GetTiles(tiles);
            _boardPainter = new BoardPainter(22, 22, GameCanvas, BlockCanvas, tileImages);

            _gameState.GameGrid[0, 0] = 1;
            _gameState.GameGrid[0, 21] = 2;
            _gameState.GameGrid[21, 0] = 3;
            _gameState.GameGrid[21, 21] = 4;
        }

        private void SetUpMatch() 
        {
            _matchClient = new MatchServiceClient(new InstanceContext(this));
            var username = SessionManager.Instance.GetUsername();

            try
            {
                _match = _matchClient.JoinToActiveMatch(username);
                _matchCode = _match.MatchCode;
                LoadInformation();

                var initialBlock = _matchClient.GetCurrentBlock(_matchCode);
                IsMyTurn(initialBlock.Color);

                SetCurrentBlock(initialBlock);
            }
            catch (Exception ex)
            {
                HandleError(Properties.Resources.JoinMatch_unableToJoin, ex);
            }
        }

        private void SetCurrentBlock(BlockDTO block)
        {
            BlockTemplate currentBlock = BlockFactory.CreateBlock(block.Block);
            currentBlock.Color = BlockFactory.MapColorToTileIndex(block.Color);

            _gameState.CurrentBlock = currentBlock;
            Draw(_gameState);
        }

        private void LoadInformation()
        {

            var playerCards = new[] { UserControl_CardOne, UserControl_CardTwo, UserControl_CardThree, UserControl_CardFour };
            var playerList = _match.Players.ToList();

            for (int i = 0; i < _match.Players.Count; i++)
            {
                var color = playerList[i].Key;
                var player = playerList[i].Value;
                playerCards[i].LoadPlayerInformation(player, color);

                _cards.Add(color, playerCards[i]);
            }
        }

        private void Draw(GameState gameStatus)
        {
            _boardPainter.DrawGrid(gameStatus.GameGrid);

            if (gameStatus.CurrentBlock != null)
            {
                _boardPainter.DrawBlock(gameStatus.CurrentBlock);
            }
        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            if (!_isMyTurn) return;

            Movement movement = Movement.None; 

            switch (e.Key)
            {
                case Key.A: case Key.Left: movement = Movement.Left; break;
                case Key.D: case Key.Right: movement = Movement.Right; break; 
                case Key.S: case Key.Down: movement = Movement.Down; break; 
                case Key.W: case Key.Up: movement = Movement.Up; break; 
                case Key.Space: HandleBlockPlacement(); break;
                default: return;
            }
            
            if (movement != Movement.None)
            {
                _matchClient.MakeMovement(_matchCode, movement);
            }
            
            MoveBlock(movement);
        }

        public void HandleBlockPlacement()
        {
            if (_gameState.CanBePlaced() && _gameState.IsValidPosition() && !_gameState.IsNextSameColor())
            {
                var result = _matchClient.PlaceBlock(_matchCode, _gameState.CurrentBlock.Punctuation);

                if (result != GameResult.Winner)
                {
                    var block = _matchClient.GetCurrentBlock(_matchCode);
                    OnBlockPlaced(); 
                    OnCurrentBlockChanged(block);
                }
                else
                {
                    OnGameFinished(result); 
                }
            }
            else
            {
                if (!_gameState.CanBePlaced()) Txt_Label.Text = Properties.Resources.GamePage_cantPlace;
                if (!_gameState.IsValidPosition()) Txt_Label.Text = Properties.Resources.GamePage_invalidPosition;
                if (_gameState.IsNextSameColor()) Txt_Label.Text = Properties.Resources.GamePage_nearSameColor;
            }
        }

        private void IsMyTurn(BlockusService.Color color)
        {
            currentColor = color; 
            var username = SessionManager.Instance.GetUsername();
            var playerTurn = _match.Players[color];

            if (playerTurn.Username == username)
            {
                _isMyTurn = true;
                Btn_Skip.Visibility = Visibility.Visible;
                Txt_Label.Visibility = Visibility.Visible;
            }
            else
            {
                _isMyTurn = false;
                Btn_Skip.Visibility = Visibility.Collapsed; 
                Txt_Label.Visibility = Visibility.Collapsed;
            }

            Txt_Turn.Text = playerTurn.Username;
        }

        private void HandleError(string message, Exception ex)
        {
            Console.WriteLine(ex.ToString());
            MessageBox.Show(Properties.Resources.Error_serverConnection);
            NavigationManager.Instance.NavigateTo(new LoginPage());
            SessionManager.Instance.LogOut();
        }

        private void MoveBlock(Movement movement)
        {
            switch (movement)
            {
                case Movement.Left: _gameState.MoveBlockLeft(); break;
                case Movement.Right: _gameState.MoveBlockRight(); break;
                case Movement.Up: _gameState.MoveBlockUp(); break;
                case Movement.Down: _gameState.MoveBlockDown(); break;
            }

            Draw(_gameState);
        }

        private void Btn_Skip_Click(object sender, RoutedEventArgs e)
        {
            var matchResult = _matchClient.SkipTurn(_matchCode);

            if (matchResult != GameResult.None)
            {
                OnGameFinished(matchResult);
                return;
            }

            var block = _matchClient.GetCurrentBlock(_matchCode);
            OnCurrentBlockChanged(block);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _matchClient.LeaveActiveMatch(SessionManager.Instance.GetUsername());
            NavigationManager.Instance.NavigateTo(new LobbyPage()); 
        }

        //Callback Methods
        public void OnCurrentBlockChanged(BlockDTO block)
        {
            IsMyTurn(block.Color);
            SetCurrentBlock(block);
        }

        public void OnOpponentMovement(Movement movement)
        {
            MoveBlock(movement);
        }

        public void OnGameFinished(GameResult gameResult)
        {
            //MessageBox.Show(SessionManager.Instance.GetUsername() + " " + gameResult.ToString());
            if (gameResult == GameResult.Winner)
            {
                NavigationManager.Instance.NavigateTo(new WinnerPage(_matchCode, _match));
            }
            else
            {
                NavigationManager.Instance.NavigateTo(new LosserPage(_matchCode, _match));
            }
        }

        public void OnBlockPlaced()
        {
            _cards[currentColor].AddPoints(_gameState.CurrentBlock.Punctuation);
            _gameState.PlaceBlock();
            Draw(_gameState);
        }

        public void OnPlayerLeave(string username, Color color)
        {
            _match.Players.Remove(color);
            _cards[color].Visibility = Visibility.Collapsed;

            MessageBox.Show(Properties.Resources.GamePage_player + username + Properties.Resources.GamePage_hasLeft);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Border_Chat.Visibility = Visibility.Visible;
            Frame_Chat.Visibility = Visibility.Visible;
        }

        private void Border_Chat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Frame_Chat.Visibility=Visibility.Collapsed;
            Border_Chat.Visibility = Visibility.Collapsed;
        }
    }
}
