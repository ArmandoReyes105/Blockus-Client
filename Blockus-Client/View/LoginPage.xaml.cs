using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
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
                    MessageBox.Show("Lo sentimos, ocurrio un error al intentarse contectar con el servidor");
                    return; 
                }

                if (account.Id == -1)
                {
                    MessageBox.Show("Lo sentimos, ocurrio un error al intentar recuperar su información");
                    return;
                }

                if (account.Id == 0)
                {
                    MessageBox.Show("Credenciales incorrectas");
                    return; 
                }

                if (account.Id == -2)
                {
                    MessageBox.Show("No se puede iniciar sesión: Tu usuario ya tiene una sesión activa");
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
                MessageBox.Show("Campos incompletos. Llene todos los campos e intente de nuevo");
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
    }
}