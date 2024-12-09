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
            InitializeButtons();

            LanguageManager.ApplyCulture();
            AnimationManager.FadeIn(this, .75);
        }

        private void FriendList(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new AccountFriendsPage());
        }

        private void AccountConfig(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new ProfileConfigurationPage());
        }

        private void CreateMatch(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new MatchMakingPage()); 
        }

        private void JoinMatch(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new JoinMatchPage()); 
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SessionManager.Instance.LogOut();
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }

        private void InitializeButtons()
        {

            if (SessionManager.Instance.IsAGuest())
            {
                btn_Config.Visibility = Visibility.Collapsed;
                btn_Friends.Visibility = Visibility.Collapsed;
            }
        }
    }
}
