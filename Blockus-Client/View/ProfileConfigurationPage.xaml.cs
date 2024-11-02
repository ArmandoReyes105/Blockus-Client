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
    /// Interaction logic for ProfileConfigurationPage.xaml
    /// </summary>
    public partial class ProfileConfigurationPage : Page
    {
        public ProfileConfigurationPage()
        {
            InitializeComponent();
        }

        private void goToLobbyPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LobbyPage());
        }
    }
}
