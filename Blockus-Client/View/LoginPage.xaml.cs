using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            AnimationManager.FadeIn(this, .75);
            AnimationManager.RotateImage(imageRotation, 12); 
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            if (AreFieldsComplete())
            {
                string password = HashManager.HashPassword(txt_Password.Password);
                var service = new AccountServiceClient();
                var account = service.Login(txt_Username.Text, password);


                if (account != null)
                {
                    MessageBox.Show("Bienvenido: " + account.Username + account.Id_Account + account.Email + account.ProfileImage + account.AccountPassword);
                    NavigationManager.Instance.NavigateTo(new LobbyPage());
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas");
                }
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
                MessageBox.Show("Campos incompletos. Llene todos los campos e intente de nuevo");
                result =  false;
            }

            return result; 
        }

        private void goToNewAccountPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new NewAccountPage());
        }

        private void ForgotPassword(object sender, RoutedEventArgs e)
        {

        }
    }
}