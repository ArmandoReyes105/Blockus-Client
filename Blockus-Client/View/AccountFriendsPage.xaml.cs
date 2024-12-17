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
            if (_friends != null)
            {
                foreach (var friend in _friends)
                {
                    var friendCard = new FriendCard();
                    friendCard.LoadFriendInformation(friend);
                    StackPanel_Friends.Children.Add(friendCard);
                }
            }
        }

        private void GoToLobbyPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new LobbyPage());
        }

        private void InitializeAFriends()
        {
            using (var client = new AccountServiceClient())
            {
                _friends = client.GetAddedFriends(SessionManager.Instance.GetCurrentAccount().Id);
            }
        }

        private List<PublicAccountDTO> GetPlayersByUsername(string username)
        {
            var searchResults = new List<PublicAccountDTO>();

            try
            {
                using (var client = new AccountServiceClient())
                {
                    searchResults = client.SearchByUsername(username).ToList();
                }
            } catch (CommunicationException ex)
            {
                log.Error("Search by username: " + ex.Message);
                HandleError(Properties.Resources.Error_serverConnection);
            } catch (TimeoutException ex)
            {
                log.Error("Search by username: " + ex.Message);
                HandleError(Properties.Resources.Error_retrievingData);
            } catch (Exception ex)
            {
                HandleError(Properties.Resources.Error_serverConnection + ex.Message);
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
