using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{

    public partial class LosserPage : Page
    {
        private MatchResumeDTO _matchResume;
        private string _matchCode;

        public LosserPage(string matchCode)
        {
            InitializeComponent();
            LanguageManager.ApplyCulture();

            _matchCode = matchCode;

            var client = new ResultsServiceClient();
            try
            {
                int id = SessionManager.Instance.GetCurrentAccount().Id;

                _matchResume = client.GetMatchResume(matchCode);
                client.UpdateResults(id, GameResult.Losser);

                Txt_Winner.Text = Properties.Resources.ResultPage_loser + _matchResume.Winner.Username;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.Error_serverConnection, ex.Message);
                NavigationManager.Instance.NavigateTo(new LoginPage());
                SessionManager.Instance.LogOut();
            }
            finally
            {
                client.Close();
            }

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LobbyPage());
        }
    }
}
