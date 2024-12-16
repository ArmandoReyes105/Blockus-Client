using Blockus_Client.Helpers;
using log4net;
using System.Data;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class LobbyPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (LobbyPage));
        private static NavigationManager _navigationManager;
        public LobbyPage()
        {
            InitializeComponent();
            InitializeButtons();

            AnimationManager.FadeIn(this, .75);
        }

        private void FriendList(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationManager.Instance.NavigateTo(new AccountFriendsPage());
            }
            catch (CommunicationException ex)
            {
                log.Error(ex.ToString());
                HandleError(Properties.Resources.Error_serverConnection);
            }
        }

        private void AccountConfig(object sender, RoutedEventArgs e)
        {
            try 
            {
                NavigationManager.Instance.NavigateTo(new ProfileConfigurationPage());
            } catch (CommunicationException ex)
            {
                log.Error(ex.ToString());
                HandleError(Properties.Resources.Error_serverConnection);
            }
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

        private void HandleError(string message)
        {
            MessageBox.Show(message);
            SessionManager.Instance.LogOut();
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }
    }
}
