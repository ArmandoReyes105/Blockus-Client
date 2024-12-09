using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using log4net;
using System.ServiceModel;

namespace Blockus_Client.UserControls
{
    public partial class UserCard : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserCard));
        private PublicAccountDTO _account;
        public UserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInformation(PublicAccountDTO account)
        {
            this._account = account;
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
                    int result = client.AddFriend(currentAccount.Id, _account.Id);
                    ShowResult(result);
                }
            }
            catch (CommunicationException ex)
            {
                log.Error("Add Friend: " + ex.Message);
                MessageBox.Show(Properties.Resources.Error_serverConnection);
                NavigationManager.Instance.NavigateTo(new LoginPage());
            }
        }

        private void ShowResult(int result)
        {
            switch (result)
            {
                case 1:
                    MessageBox.Show(Properties.Resources.Message_addSuccess);
                    break;

                case 0:
                    MessageBox.Show(Properties.Resources.Message_alreadyFriends);
                    break;

                case -2:
                    MessageBox.Show(Properties.Resources.Message_isYou);
                    break;

                default:
                    MessageBox.Show(Properties.Resources.Message_genericError);
                    break;
            }

            NavigationManager.Instance.NavigateTo(new AccountFriendsPage());
        }
    }
}
