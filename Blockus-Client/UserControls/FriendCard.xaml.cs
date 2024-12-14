using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.View;
using log4net;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blockus_Client.UserControls
{
    public partial class FriendCard : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FriendCard));
        private PublicAccountDTO _friend;
        private ResultsDTO _results;
        public FriendCard()
        {
            InitializeComponent();
        }

        public void ResetInformation()
        {
            Image_FriendProfileImage.Visibility = Visibility.Hidden;
            btn_Remove.Visibility = Visibility.Hidden;
        }

        public void LoadFriendInformation(PublicAccountDTO friend)
        {
            this._friend = friend;
            LoadProfileImage();
            GetFriendResults();

            txt_FriendName.Text = _friend.Username;
            txt_Victories.Text = ": " + _results.Victories.ToString();
            txt_Losses.Text = ": " + _results.Losses.ToString(); 
        }

        public void DeleteFriend(object sender, RoutedEventArgs e)
        {
            try
            {
                using (AccountServiceClient client = new AccountServiceClient())
                {
                    AccountDTO currentAccount = SessionManager.Instance.GetCurrentAccount();
                    client.DeleteFriend(_friend.Id, currentAccount.Id);

                    NavigationManager.Instance.NavigateTo(new AccountFriendsPage());
                }
            }
            catch (CommunicationException ex)
            {
                log.Error("Delete friend: " + ex.Message);
                MessageBox.Show(Properties.Resources.Error_UpdateData);
            }
        }

        private void LoadProfileImage()
        {
            var imageMapping = new Dictionary<int, string>
            {
                { 1, "/Blockus-Client;component/Resources/Images/ProfileImage-1.png" },
                { 2, "/Blockus-Client;component/Resources/Images/ProfileImage-2.png" }
            };

            if (imageMapping.TryGetValue(_friend.ProfileImage, out string imagePath))
            {
                Uri uri = new Uri(imagePath, UriKind.Relative);
                Image_FriendProfileImage.Source = new BitmapImage(uri);
            }
        }

        private void GetFriendResults()
        {
            try
            {
                using (AccountServiceClient client = new AccountServiceClient())
                {
                    _results = client.GetAccountResults(_friend.Id);
                }
            }
            catch (CommunicationException ex)
            {
                log.Error("Get friend results: " + ex.Message);
                MessageBox.Show(Properties.Resources.Error_UpdateData);
            }
        }
    }
}
