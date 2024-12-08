using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System.Windows.Controls;
using System;
using System.Windows;

namespace Blockus_Client.View
{
    public partial class WinnerPage : Page
    {

        private MatchResumeDTO _matchResume;
        private string _matchCode; 

        public WinnerPage(string matchCode)
        {
            InitializeComponent();

            _matchCode = matchCode;

            var client = new ResultsServiceClient(); 
            try
            {
                int id = SessionManager.Instance.GetCurrentAccount().Id;

                _matchResume = client.GetMatchResume(matchCode);
                client.UpdateResults(id, GameResult.Winner);

                Txt_Winner.Text = "Winner " + _matchResume.Winner.Username; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo sentimos ha ocurrido un error al intentar comunicarse con el servidor", ex.Message);
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
