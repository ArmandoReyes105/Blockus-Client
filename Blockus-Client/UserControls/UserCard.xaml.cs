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
    public partial class UserCard : UserControl
    {
        public PublicAccountDTO account;
        public UserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInformation(PublicAccountDTO account)
        {
            this.account = account;
            txt_Username.Text = account.Username;

            var imageMapping = new Dictionary<int, string>
            {
                { 1, "/Blockus-Client;component/Resources/Images/ProfileImage-1.png" },
                { 2, "/Blockus-Client;component/Resources/Images/ProfileImage-2.png" }
            };

            if (imageMapping.TryGetValue(account.ProfileImage, out string imagePath))
            {
                Uri uri = new Uri(imagePath, UriKind.Relative);
                Image_UserProfileImage.Source = new BitmapImage(uri);
            }
        }

        private void AddNewFriend(object sender, RoutedEventArgs e)
        {
            try
            {
                using (AccountServiceClient client = new AccountServiceClient())
                {
                    AccountDTO currentAccount = SessionManager.Instance.GetCurrentAccount();
                    int result = client.AddFriend(currentAccount.Id, account.Id);

                    switch (result)
                    {
                        case 1: // Success
                            MessageBox.Show("Friend added successfully!");
                            break;

                        case 0: // Already friends
                            MessageBox.Show("You are already friends with this user.");
                            break;

                        case -2: // Adding oneself
                            MessageBox.Show("You cannot add yourself as a friend.");
                            break;

                        case -1: // Generic error
                        default:
                            MessageBox.Show("An error occurred. Please try again.");
                            break;
                    }

                    NavigationManager.Instance.NavigateTo(new AccountFriendsPage());
                }
            } catch (EntityException ex)
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
