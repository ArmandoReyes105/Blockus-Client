using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class ProfileConfigurationPage : Page
    {
        public ProfileConfigurationPage()
        {
            LanguageManager.ApplyCulture();
            InitializeComponent();
            try
            {
                InitializeAccountInformation();
            }
            catch (Exception e)
            {
                MessageBox.Show(Properties.Resources.Error_serverConnection);
                Console.WriteLine(e.Message);
                SessionManager.Instance.LogOut();
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }

        }

        private void InitializeAccountInformation()
        {
            AccountServiceClient client = new AccountServiceClient();

            AccountDTO account = SessionManager.Instance.GetCurrentAccount();
            ResultsDTO results = client.GetAccountResults(account.Id);
            ProfileConfigurationDTO profile = client.GetProfileConfiguration(account.Id);
            client.Close();

            txt_Username.Text = account.Username;
            txt_Email.Text = account.Email;

            if (results.Id != 0)
            {
                txt_Victories.Text = results.Victories.ToString();
                txt_Losses.Text = results.Losses.ToString();
            }
        }

        private void GoToChangePassword(object sender, RoutedEventArgs e)
        {
            //ASK FIRST
            NavigationManager.Instance.NavigateTo(new ChangePasswordPage());
        }

        private void GoToLobbyPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LobbyPage());
        }

        private void ChangeBoardStyle(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChangeTilesStyle(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
