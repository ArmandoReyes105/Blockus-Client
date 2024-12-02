﻿using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
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

namespace Blockus_Client.UserControls
{
    public partial class FriendCard : UserControl
    {
        public PublicAccountDTO friend;
        public FriendCard()
        {
            InitializeComponent();
        }

        public void ResetInformation()
        {
            Image_FriendProfileImage.Visibility = Visibility.Hidden;
            txt_FriendName.Text = "You can add up to 10 friend. ";
            btn_Remove.Visibility = Visibility.Hidden;
        }

        public void LoadFriendInformation(PublicAccountDTO friend)
        {
            this.friend = friend;
            txt_FriendName.Text = friend.Username;

            var imageMapping = new Dictionary<int, string>
            {
                { 1, "/Blockus-Client;component/Resources/Images/ProfileImage-1.png" },
                { 2, "/Blockus-Client;component/Resources/Images/ProfileImage-2.png" }
            };

            if (imageMapping.TryGetValue(friend.ProfileImage, out string imagePath))
            {
                Uri uri = new Uri(imagePath, UriKind.Relative);
                Image_FriendProfileImage.Source = new BitmapImage(uri);
            }
        }

        public void DeleteFriend(object sender, RoutedEventArgs e)
        {
            try
            {
                using (AccountServiceClient client = new AccountServiceClient())
                {
                    AccountDTO currentAccount = SessionManager.Instance.GetCurrentAccount();
                    client.DeleteFriend(friend.Id ,currentAccount.Id);

                    NavigationManager.Instance.NavigateTo(new AccountFriendsPage());
                }
            } catch (EntityException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (SqlException ex )
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}