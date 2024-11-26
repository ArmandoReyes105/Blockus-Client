﻿using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.UserControls;
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
    public partial class AccountFriendsPage : Page
    {
        private PublicAccountDTO[] friends;
        
        public AccountFriendsPage()
        {
            InitializeComponent();
            try
            {
                AccountServiceClient client = new AccountServiceClient();
                friends = client.GetAddedFriends(SessionManager.Instance.GetCurrentAccount().Id);
                InitializeFriendsInformation();
            } catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al comunicarse con el servidor. Intente mas tarde.\n");
                Console.WriteLine(ex.Message);
                SessionManager.Instance.LogOut();
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }
        }


        private void InitializeFriendsInformation()
        {
            foreach (var friend in friends)
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
    }
}
