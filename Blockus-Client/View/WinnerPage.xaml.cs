using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System.Windows.Controls;
using System.Windows;
using log4net;
using System.ServiceModel;
using System.Collections.Generic;
using System;
using System.Windows.Media.Imaging;
using Blockus_Client.UserControls;
using System.Windows.Media;

namespace Blockus_Client.View
{
    public partial class WinnerPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WinnerPage));
        private MatchResumeDTO _matchResume;
        private readonly MatchDTO _match;

        public WinnerPage(string matchCode, MatchDTO match)
        {
            InitializeComponent();

            _match = match; 

            GetMatchResume(matchCode);
            SetWinnerInformation();
            LoadLossers();
            LoadPunctuationTable();
            LoadColor(_matchResume.Winner.Color);

            if (!SessionManager.Instance.IsAGuest())
            {
                UpdatePlayerResults();
            }

        }

        private void NavigateToLobby(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LobbyPage()); 
        }

        private void GetMatchResume(string matchCode)
        {
            var client = new ResultsServiceClient();

            try
            {
                _matchResume = client.GetMatchResume(matchCode);
            }
            catch (CommunicationException ex)
            {
                log.Error("Get match resume: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection);
            }
            finally
            {
                client.Close();
            }
        }

        private void UpdatePlayerResults()
        {
            var client = new ResultsServiceClient();
            int id = SessionManager.Instance.GetCurrentAccount().Id;

            try
            {
                int result = client.UpdateResults(id, GameResult.Winner);

                if (result == 0)
                {
                    MessageBox.Show(Properties.Resources.Error_UpdateData);
                }

            }
            catch (CommunicationException ex)
            {
                log.Error("Update results: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection);
            }
            finally
            {
                client.Close();
            }
        }

        private void SetWinnerInformation()
        {
            Txt_Punctuation.Text = _matchResume.Winner.Puntuation.ToString();
            LoadProfileImage();   
        }

        private void HandleError(string message)
        {
            MessageBox.Show(message);
            NavigationManager.Instance.NavigateTo(new LoginPage());
            SessionManager.Instance.LogOut();
        }

        private void LoadProfileImage()
        {

            var winner = _match.Players[_matchResume.Winner.Color];

            var imageMapping = new Dictionary<int, string>
            {
                { 1, "/Blockus-Client;component/Resources/Images/ProfileImage-1.png" },
                { 2, "/Blockus-Client;component/Resources/Images/ProfileImage-2.png" }
            };

            if (imageMapping.TryGetValue(winner.ProfileImage, out string imagePath))
            {
                Uri uri = new Uri(imagePath, UriKind.Relative);
                Image_Winner.Source = new BitmapImage(uri);
            }
        }

        private void LoadLossers()
        {
            foreach (var player in _match.Players)
            {
                if (player.Value.Username != _matchResume.Winner.Username)
                {
                    var card = new ActivePlayerCard();
                    card.LoadPlayerInformation(player.Value, player.Key);
                    card.Width = 150;
                    card.Height = 150;
                    card.Margin = new Thickness(10);
                    WrapPanel_Lossers.Children.Add(card);
                }
            }
        }

        private void LoadPunctuationTable()
        {
            foreach (var player in _matchResume.PlayerList)
            {
                var card = new PunctuationCard();
                card.SetInformation(player.Username, player.Puntuation, player.Color);
                StackPanel_Punctuation.Children.Add(card);
            }
        }

        private void LoadColor(Blockus_Client.BlockusService.Color color)
        {
            var colorMapping = new Dictionary<BlockusService.Color, string>
            {
                { BlockusService.Color.Red, "Red" },
                { BlockusService.Color.Blue, "Blue" },
                { BlockusService.Color.Yellow, "Yellow" },
                { BlockusService.Color.Green, "Green" }
            };

            var colorType = colorMapping.ContainsKey(color) ? colorMapping[color] : "Gray";
            SolidColorBrush brush = (SolidColorBrush)Application.Current.Resources[colorType];
            GradientStop_PlayerColor.Color = brush.Color;
        }
    }
}
