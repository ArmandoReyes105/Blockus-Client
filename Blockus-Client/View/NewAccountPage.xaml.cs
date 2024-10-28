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
            RotateImage(imageRotation); 
        }

        private void RotateImage(RotateTransform rotateTransform)
        {
            DoubleAnimation rotationAnimation = new DoubleAnimation
            {
                From = 0,              
                To = 360,              
                Duration = TimeSpan.FromSeconds(10),
                RepeatBehavior = RepeatBehavior.Forever 
            };

            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotationAnimation);
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

            //var accountClient = new AccountServiceClient();
            //int result = accountClient.CreateAccount(account);
            int result = 1; 
            //accountClient.Close();

            if (result != 0)
            {
                MessageBox.Show("Su cuenta ah sido creada exitosamente", "Cuenta creada", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Ah ocurrido un error al intentar crear su cuenta", "Error al crear cuenta", MessageBoxButton.OK); 
            }
        }
    }
}
