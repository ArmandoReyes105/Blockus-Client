using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blockus_Client.UserControls
{
    public partial class FriendCard : UserControl
    {
        public PublicAccountDTO friend;
        public FriendCard()
        {
            InitializeComponent();
            LanguageManager.ApplyCulture();
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
                    client.DeleteFriend(friend.Id, currentAccount.Id);

                    NavigationManager.Instance.NavigateTo(new AccountFriendsPage());
                }
            }
            catch (EntityException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
