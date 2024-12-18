using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.UserControls;
using log4net;
using System.Collections.Generic;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Linq;

namespace Blockus_Client.View
{

    public partial class LosserPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WinnerPage));
        private BlockusService.Color _myColor; 
        private MatchResumeDTO _matchResume;
        private readonly MatchDTO _match;

        public LosserPage(string matchCode, MatchDTO match)
        {
            InitializeComponent();

            _match = match;

            GetMatchResume(matchCode);
            SetMyInformation();
            LoadLossers();
            LoadPunctuationTable();

            Card_Winner.LoadPlayerInformation(_match.Players[_matchResume.Winner.Color], _matchResume.Winner.Color);
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

            try
            {
                using (var client = new ResultsServiceClient())
                {
                    _matchResume = client.GetMatchResume(matchCode);
                }
            }
            catch (CommunicationException ex)
            {
                log.Error("Get match resume: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection);
            }
            catch (Exception ex)
            {
                log.Error("Get match resume: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection);
            }
        }

        private void UpdatePlayerResults()
        {
            int id = SessionManager.Instance.GetCurrentAccount().Id;
            int result; 

            try
            {
                using (var client = new ResultsServiceClient())
                {
                    result = client.UpdateResults(id, GameResult.Losser);
                }

                if (result == -1)
                {
                    MessageBox.Show(Properties.Resources.Error_UpdateData);
                }

            }
            catch (CommunicationException ex)
            {
                log.Error("Update results: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection);
            }
            catch (Exception ex)
            {
                log.Error("Get match resume: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection);
            }
        }

        private void SetMyInformation()
        {
            var username = SessionManager.Instance.GetUsername();
            _myColor = _match.Players.FirstOrDefault(x => x.Value.Username == username).Key;
            Txt_Punctuation.Text = _matchResume.PlayerList.Where(x => x.Username == username).FirstOrDefault().Puntuation.ToString();
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
            var myState = _match.Players[_myColor];

            var imageMapping = new Dictionary<int, string>
            {
                { 1, "/Blockus-Client;component/Resources/Images/ProfileImage-1.png" },
                { 2, "/Blockus-Client;component/Resources/Images/ProfileImage-2.png" }
            };

            if (imageMapping.TryGetValue(myState.ProfileImage, out string imagePath))
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
    }
}
