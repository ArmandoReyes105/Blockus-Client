using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using log4net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{

    public partial class LosserPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LosserPage));
        private MatchResumeDTO _matchResume;

        public LosserPage(string matchCode)
        {
            InitializeComponent();
            GetMatchResume(matchCode);
            Txt_Winner.Text = Properties.Resources.ResultPage_loser + _matchResume.Winner.Username;

            if (!SessionManager.Instance.IsAGuest())
            {
                UpdatePlayerResults();
            }
            
        }

        private void NavigateToLobby(object sender, System.Windows.RoutedEventArgs e)
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
                HandleServerError();
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
                int result = client.UpdateResults(id, GameResult.Losser);
                if (result == 0)
                {
                    MessageBox.Show(Properties.Resources.Error_UpdateData);
                }
            }
            catch (CommunicationException ex)
            {
                log.Error("Update results: " + ex.Message);
                HandleServerError();
            }
            finally
            {
                client.Close();
            }
        }

        private void HandleServerError()
        {
            MessageBox.Show(Properties.Resources.Error_serverConnection);
            NavigationManager.Instance.NavigateTo(new LoginPage());
            SessionManager.Instance.LogOut();
        }
    }
}
