using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blockus_Client.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            if (completeFields())
            {
                var service = new AccountServiceClient();
                var account = service.Login(txt_Username.Text, txt_Password.Password);

                string password = HashManager.HashPassword(txt_Password.Password);

                if (account != null)
                {
                    MessageBox.Show("Bienvenido: " + account.Username);
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

        private bool completeFields()
        {
            if (string.IsNullOrEmpty(txt_Username.Text) || string.IsNullOrEmpty(txt_Password.Password))
            {
                MessageBox.Show("Campos incompletos. Llene todos los campos e intente de nuevo");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void goToNewAccountPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new NewAccountPage());
        }
    }
}