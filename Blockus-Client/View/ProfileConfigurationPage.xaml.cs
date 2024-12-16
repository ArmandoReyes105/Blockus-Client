using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using log4net;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class ProfileConfigurationPage : Page
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ProfileConfigurationPage));

        public ProfileConfigurationPage()
        {
            InitializeComponent();
            InitializeTiles();

            try
            {
                InitializeAccountInformation();
            }
            catch (Exception e)
            {
                log.Error("Get Account/Results/Configuration info: " + e.Message);
                MessageBox.Show(Properties.Resources.Error_serverConnection);
                Console.WriteLine(e.Message);
                SessionManager.Instance.LogOut();
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }

        }

        private void InitializeAccountInformation()
        {
            using (var client = new AccountServiceClient())
            {
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
        }

        private void GoToChangePassword(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new ChangePasswordPage());
        }

        private void GoToLobbyPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LobbyPage());
        }

        private void ChangeTilesStyle(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton clickedButton)
            {
                
                switch (clickedButton.Name)
                {
                    case nameof(Rbtn_Normal):
                        SessionManager.Instance.SetTilesConfiguration(1);
                        break;

                    case nameof(Rbtn_Minimal):
                        SessionManager.Instance.SetTilesConfiguration(2);
                        break;

                    case nameof(Rbtn_Art):
                        SessionManager.Instance.SetTilesConfiguration(3);
                        break;

                    case nameof(Rbtn_Anniversary):
                        SessionManager.Instance.SetTilesConfiguration(4);
                        break;
                }
            }
        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton clickedButton)
            {
                switch (clickedButton.Name)
                {
                    case nameof(Rbtn_English):
                        LanguageManager.SetLanguageToEnglish();
                        break;

                    case nameof(Rbtn_Espanish):
                        LanguageManager.SetLanguageToSpanish();
                        break;
                }

                NavigationManager.Instance.NavigateTo(new ProfileConfigurationPage());
            }
        }

        private void InitializeTiles()
        {
            int tiles = SessionManager.Instance.GetTilesConfiguration();

            switch (tiles)
            {
                case 1: Rbtn_Normal.IsChecked = true; break;
                case 2: Rbtn_Minimal.IsChecked = true; break;
                case 3: Rbtn_Art.IsChecked = true; break;
                case 4: Rbtn_Anniversary.IsChecked = true; break;
            }
        }
    }
}
