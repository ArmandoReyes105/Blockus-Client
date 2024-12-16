using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System.Windows;
using System.Windows.Controls;
using Blockus_Client.Validations;
using System.ServiceModel;
using log4net;
using System.ServiceModel;
using System.Security.Principal;
using System;

namespace Blockus_Client.View
{
    public partial class NewAccountPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(NewAccountPage));
        private readonly string _code;
        private AccountDTO _account;

        public NewAccountPage()
        {
            InitializeComponent();
            AnimationManager.FadeIn(this, .75);
            AnimationManager.RotateImage(imageRotation, 10);

            _code = RandomManager.GenerateRandomCode();
        }

        private void CreateAccount(object sender, RoutedEventArgs e)
        {

            _account = new AccountDTO
            {
                Username = txt_Username.Text,
                Email = txt_Email.Text,
                Password = txt_Password.Password
            };

            var accountValidator = new AccountValidator(_account, txt_PasswordConfirmation.Password);
            var validationResults = accountValidator.Validate();

            if (validationResults.Count > 0)
            {
                MessageBox.Show(validationResults[0].ErrorMessage, Properties.Resources.Register_incorrectData, MessageBoxButton.OK);
                return;
            }

            EmailManager.SendVerificationEmail(_account.Email, _code);
            Txt_Email.Text = _account.Email;
            StackPanel_Form.Visibility = Visibility.Collapsed;
            StackPanel_Code.Visibility = Visibility.Visible;
        }

        private void GoToLogin(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }

        private void CreateAccount(AccountDTO account)
        {
            string password = HashManager.HashPassword(txt_Password.Password);
            account.Password = password;
            int result; 

<<<<<<< HEAD
            int result = SaveAccount(account); 
=======
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
>>>>>>> 8c6696c6f00414c1d3b1c6540337d23bbd7eb307

            if (result != 0)
            {
                MessageBox.Show(Properties.Resources.Register_success, Properties.Resources.Register_creationSuccess, MessageBoxButton.OK);
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }

            if (result == 0)
            {
                MessageBox.Show(Properties.Resources.Error_unsuccesfulOperation, Properties.Resources.Register_creationFailure, MessageBoxButton.OK);
            }
<<<<<<< HEAD

=======
            
>>>>>>> 8c6696c6f00414c1d3b1c6540337d23bbd7eb307
            if (result == -1)
            {
                MessageBox.Show(Properties.Resources.Error_serverConnection);
            }
<<<<<<< HEAD
        }

        private int SaveAccount(AccountDTO account)
        {
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
                log.Error("Create Account: " + ex.Message);
                result = -1;
            }
            catch (TimeoutException ex)
            {
                log.Error("Create Account: " + ex.Message);
                result = -1;
            }
            catch (Exception ex)
            {
                log.Error("Create Account: " + ex.Message);
                result = -1;
            }

            return result; 
=======
>>>>>>> 8c6696c6f00414c1d3b1c6540337d23bbd7eb307
        }

        private void ReturnToForm(object sender, RoutedEventArgs e)
        {
            StackPanel_Form.Visibility = Visibility.Visible;
            StackPanel_Code.Visibility = Visibility.Collapsed;
        }

        private void VerifyCode(object sender, RoutedEventArgs e)
        {
            if (TextBox_Code.Text == _code)
            {
                CreateAccount(_account);
            }
            else
            {
                MessageBox.Show("El código ingresado es incorrecto");
            }
        }
    }
}
