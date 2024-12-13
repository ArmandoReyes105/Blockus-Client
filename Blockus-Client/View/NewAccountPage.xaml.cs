using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Blockus_Client.Validations;
using System.ServiceModel;

namespace Blockus_Client.View
{
    public partial class NewAccountPage : Page
    {
        public NewAccountPage()
        {
            InitializeComponent();
            LanguageManager.ApplyCulture();
            AnimationManager.FadeIn(this, .75);
            AnimationManager.RotateImage(imageRotation, 10);
        }

        private void CreateAccount(object sender, RoutedEventArgs e)
        {

            var account = new AccountDTO
            {
                Username = txt_Username.Text,
                Email = txt_Email.Text,
                Password = txt_Password.Password
            };

            var accountValidator = new AccountValidator(account, txt_PasswordConfirmation.Password);
            var validationResults = accountValidator.Validate();

            if (validationResults.Count > 0)
            {
                MessageBox.Show(validationResults[0].ErrorMessage, Properties.Resources.Register_incorrectData, MessageBoxButton.OK);
                return;
            }

            string password = HashManager.HashPassword(txt_Password.Password);
            account.Password = password;
            int result; 

            try
            {
                using (var accountClient = new AccountServiceClient())
                {
                    result = accountClient.CreateAccount(account);
                }
            }
            catch (CommunicationException ex)
            {
                result = -1; 
            }

            if (result != 0)
            {
                MessageBox.Show(Properties.Resources.Register_success, Properties.Resources.Register_creationSuccess, MessageBoxButton.OK);
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }

            if (result == 0)
            {
                MessageBox.Show(Properties.Resources.Error_unsuccesfulOperation, Properties.Resources.Register_creationFailure, MessageBoxButton.OK);
            }
            
            if (result == -1)
            {
                MessageBox.Show(Properties.Resources.Error_serverConnection);
            }
        }

        private void goToLogin(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }
    }
}
