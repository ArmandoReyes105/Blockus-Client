using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System.Windows.Controls;
using System.Windows;
using log4net;
using System.ServiceModel;

namespace Blockus_Client.View
{
    public partial class WinnerPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WinnerPage));
        private MatchResumeDTO _matchResume; 

        public WinnerPage(string matchCode)
        {
            InitializeComponent();
            GetMatchResume(matchCode);
            Txt_Winner.Text = Properties.Resources.ResultPage_loser + " " + _matchResume.Winner.Username;

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

        private void HandleError(string message)
        {
            MessageBox.Show(message);
            NavigationManager.Instance.NavigateTo(new LoginPage());
            SessionManager.Instance.LogOut();
        }
    }
}
