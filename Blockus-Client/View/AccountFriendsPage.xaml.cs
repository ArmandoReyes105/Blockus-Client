using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.UserControls;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class AccountFriendsPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AccountFriendsPage));
        private PublicAccountDTO[] _friends;

        public AccountFriendsPage()
        {
            InitializeComponent();
            InitializeAFriends();
            InitializeFriendsInformation();

        }


        private void InitializeFriendsInformation()
        {
            foreach (var friend in _friends)
            {
                var friendCard = new FriendCard();
                friendCard.LoadFriendInformation(friend);
                StackPanel_Friends.Children.Add(friendCard);
            }
        }

        private void GoToLobbyPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LobbyPage());
        }

        private void InitializeAFriends()
        {
            AccountServiceClient client = new AccountServiceClient();

            try
            {
                _friends = client.GetAddedFriends(SessionManager.Instance.GetCurrentAccount().Id);
            }
            catch (Exception ex)
            {
                log.Error("Get added friends: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection);
            }
            finally
            {
                client.Close(); 
            }
        }

        private List<PublicAccountDTO> GetPlayersByUsername(string username)
        {
            var client = new AccountServiceClient();
            var searchResults = new List<PublicAccountDTO>();

            try
            {
                searchResults = client.SearchByUsername(username).ToList();
            }
            catch (CommunicationException ex)
            {
                log.Error("Search by username: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection); 
            }
            finally
            {
                client.Close();
            }

            return searchResults; 
        }

        private void HandleError(string message)
        {
            MessageBox.Show(message);
            SessionManager.Instance.LogOut();
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }

        private void SearchPlayers(object sender, TextChangedEventArgs e)
        {
            string username = txt_searchFriends.Text.Trim();
            var players = GetPlayersByUsername(username);

            WrapPanel_AddFriends.Children.Clear();

            foreach (var player in players)
            {
                var userCard = new UserCard();
                userCard.LoadUserInformation(player);
                WrapPanel_AddFriends.Children.Add(userCard);
            }
        }
    }
}
