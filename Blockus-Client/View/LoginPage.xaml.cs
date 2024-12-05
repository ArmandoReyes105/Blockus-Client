using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            LanguageManager.ApplyCulture();
            AnimationManager.FadeIn(this, .75);
            AnimationManager.RotateImage(imageRotation, 12); 
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            if (AreFieldsComplete())
            {
                string password = HashManager.HashPassword(txt_Password.Password);
                AccountDTO account; 

                try
                {
                    var service = new SessionServiceClient();
                    account = await service.LogInAsync(txt_Username.Text, password);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    account = null; 
                }
                
                if (account == null)
                {
                    MessageBox.Show(Properties.Resources.Error_serverConnection);
                    return; 
                }

                if (account.Id == -1)
                {
                    MessageBox.Show(Properties.Resources.Error_retrievingData);
                    return;
                }

                if (account.Id == 0)
                {
                    MessageBox.Show(Properties.Resources.Error_wrongCredentials);
                    return; 
                }

                if (account.Id == -2)
                {
                    MessageBox.Show(Properties.Resources.Error_alreadyLogged);
                    return; 
                }

                SessionManager.Instance.LogIn(account); 
                NavigationManager.Instance.NavigateTo(new LobbyPage());
            }
        }

        private void LoginAsGuest(object sender, RoutedEventArgs e)
        {
            //TODO
            //TODO again
        }

        private bool AreFieldsComplete()
        {
            bool result = true; 

            if (string.IsNullOrEmpty(txt_Username.Text) || string.IsNullOrEmpty(txt_Password.Password))
            {
                MessageBox.Show(Properties.Resources.Error_incompleteFields);
                result =  false;
            }

            return result; 
        }

        private void GoToNewAccountPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new NewAccountPage());
        }

        private void ForgotPassword(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            LanguageManager.CurrentLanguage = LanguageManager.CurrentLanguage == "es-MX" ? "" : "es-MX";

            NavigationManager.Instance.NavigateTo(new LoginPage());
        }
    }
}