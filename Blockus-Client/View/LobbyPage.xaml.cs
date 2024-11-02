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
    /// Interaction logic for LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : Page
    {
        public LobbyPage()
        {
            InitializeComponent();
        }

        private void FriendList(object sender, RoutedEventArgs e)
        {

        }

        private void AccountConfig(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new ProfileConfigurationPage());
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }
        }

        private void CreateMatch(object sender, RoutedEventArgs e)
        {

        }

        private void JoinMatch(object sender, RoutedEventArgs e)
        {

        }
    }
}
