using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class ChangePasswordPage : Page
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
            AnimationManager.FadeIn(this, .75);
            AnimationManager.RotateImage(imageRotation, 10);
        }

        private void UpdateAccountPassword(object sender, RoutedEventArgs e)
        {
            if (!AreFieldsComplete())
                return;

            string enteredPassword = HashManager.HashPassword(txt_Password.Password);
            string newPassword = txt_NewPassword.Password;

            if (!ValidateNewPassword(newPassword) || !CheckPassword(sender, e))
                return;

            string hashedNewPassword = HashManager.HashPassword(newPassword);

            AccountDTO currentAccount = SessionManager.Instance.GetCurrentAccount();
            if (currentAccount == null)
            {
                MessageBox.Show(Properties.Resources.Error_noConnection);
                return;
            }

            currentAccount.Password = hashedNewPassword;

            try
            {
                using (var accountClient = new AccountServiceClient())
                {
                    int result = accountClient.UpdateAccount(currentAccount);
                    accountClient.Close();

                    if (result > 0)
                    {
                        MessageBox.Show(Properties.Resources.ModifyPassword_passwordModified);
                        SessionManager.Instance.LogOut();
                        NavigationManager.Instance.NavigateTo(new LoginPage());
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.Error_unsuccesfulOperation);
                    }
                }
            } catch (CommunicationException)
            {
                HandleError(Properties.Resources.Error_serverConnection);
                return;
            } catch (TimeoutException)
            {
                HandleError(Properties.Resources.Error_serverConnection);
                return;
            }
            
        }

        private void GoToProfilePage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new ProfileConfigurationPage());
        }

        private bool CheckPassword(object sender, RoutedEventArgs e)
        {
            AccountDTO currentAccount = SessionManager.Instance.GetCurrentAccount();
            if (currentAccount == null)
            {
                MessageBox.Show(Properties.Resources.Error_noConnection);
                return false;
            }

            string enteredPassword = HashManager.HashPassword(txt_Password.Password);
            string newPassword = txt_NewPassword.Password;

            if (enteredPassword != currentAccount.Password)
            {
                MessageBox.Show(Properties.Resources.Error_wrongCredentials);
                return false;
            }

            if (enteredPassword == newPassword)
            {
                MessageBox.Show(Properties.Resources.Error_samePassword);
                return false;
            }

            return true;
        }

        private bool ValidateNewPassword(string newPassword)
        {
            if (newPassword.Length < 8 || newPassword.Length > 16)
            {
                MessageBox.Show(Properties.Resources.Error_samePassword);
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(newPassword, @"^[a-zA-z0-9]+$"))
            {
                MessageBox.Show(Properties.Resources.Error_specialCharacters);
                return false;
            }

            return true;
        }

        private bool AreFieldsComplete()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txt_Password.Password) || string.IsNullOrEmpty(txt_NewPassword.Password))
            {
                MessageBox.Show(Properties.Resources.Error_incompleteFields);
                result = false;
            }

            return result;
        }

        private void HandleError(string message)
        {
            MessageBox.Show(message);
            SessionManager.Instance.LogOut();
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }
    }
}
