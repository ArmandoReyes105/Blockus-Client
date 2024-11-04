using Blockus_Client.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class LobbyPage : Page
    {
        public LobbyPage()
        {
            InitializeComponent();

            AnimationManager.FadeIn(this, .75); 
        }

        private void FriendList(object sender, RoutedEventArgs e)
        {

        }

        private void AccountConfig(object sender, RoutedEventArgs e)
        {

        }

        private void CreateMatch(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new MatchMakingPage()); 
        }

        private void JoinMatch(object sender, RoutedEventArgs e)
        {
            
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SessionManager.Instance.LogOut();
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }
    }
}
