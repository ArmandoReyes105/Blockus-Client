using System.Windows;
using System.Windows.Controls;
using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;

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
            string enteredPassword = HashManager.HashPassword(txt_Password.Password);
            string newPassword = HashManager.HashPassword(txt_NewPassword.Password);

            if (!CheckPassword(sender, e))
                return;

            AccountDTO currentAccount = SessionManager.Instance.GetCurrentAccount();
            if (currentAccount == null)
            {
                MessageBox.Show("Ocurrio un error al conectarse al sistema, intente mas tarde");
                return;
            }

            currentAccount.Password = newPassword;

            var accountClient = new AccountServiceClient();
            int result = accountClient.UpdateAccount(currentAccount);
            accountClient.Close();

            if (result > 0)
            {
                MessageBox.Show("Su contrasenia ha sido actualizada exitosamente");
                SessionManager.Instance.LogOut();
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }
            else
            {
                MessageBox.Show("Error al actualizar los datos, intente nuevamente");
            }
        }

        private void GoToProfilePage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new ProfileConfigurationPage());
        }

        private bool CheckPassword(object sender, RoutedEventArgs e)
        {
            if (!AreFieldsComplete())
                return false;

            AccountDTO currentAccount = SessionManager.Instance.GetCurrentAccount();
            if (currentAccount == null)
            {
                MessageBox.Show("Ocurrio un error al conectarse al sistema, intente mas tarde");
                return false;
            }

            string enteredPassword = HashManager.HashPassword(txt_Password.Password);
            string newPassword = HashManager.HashPassword(txt_NewPassword.Password);

            if (enteredPassword != currentAccount.Password)
            {
                MessageBox.Show("Tu contrasenia no coincide");
                return false;
            }

            if (enteredPassword == newPassword)
            {
                MessageBox.Show("La nueva contrasenia no puede ser igual a la actual");
                return false;
            }

            return true;
        }

        private bool AreFieldsComplete()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txt_Password.Password) || string.IsNullOrEmpty(txt_NewPassword.Password))
            {
                MessageBox.Show("Campos incompletos. Llene todos los campos e intente nuevamente.");
                result = false;
            }

            return result;
        }
    }
}
