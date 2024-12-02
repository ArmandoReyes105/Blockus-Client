using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Blockus_Client.Validations;

namespace Blockus_Client.View
{
    public partial class NewAccountPage : Page
    {
        public NewAccountPage()
        {
            InitializeComponent();
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
                MessageBox.Show(validationResults[0].ErrorMessage, "Datos incorrectos", MessageBoxButton.OK);
                return; 
            }

            string password = HashManager.HashPassword(txt_Password.Password);
            account.Password = password;

            var accountClient = new AccountServiceClient();
            int result = accountClient.CreateAccount(account);
            accountClient.Close();

            if (result != 0)
            {
                MessageBox.Show("Su cuenta ah sido creada exitosamente", "Cuenta creada", MessageBoxButton.OK);
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }
            else
            {
                MessageBox.Show("Ah ocurrido un error al intentar crear su cuenta", "Error al crear cuenta", MessageBoxButton.OK); 
            }
        }

        private void goToLogin(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }
    }
}
