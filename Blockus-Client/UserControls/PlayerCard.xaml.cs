using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using Blockus_Client.Interfaces;
using Blockus_Client.View;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Blockus_Client.UserControls
{
    public partial class PlayerCard : UserControl
    {
        private BlockusService.Color playerColor;
        private KickerPlayer matchPage;
        private MatchMakingServiceClient client;
        private PublicAccountDTO account;
        public PlayerCard()
        {
            InitializeComponent();
        }

        public void ResetInformation()
        {
            btn_KickPlayer.Visibility = Visibility.Collapsed;
            this.client = null;
            this.account = null;

            SolidColorBrush brush = (SolidColorBrush)Application.Current.Resources["Gray"];

            Border_Stroke.BorderBrush = brush;
            GradientStop_PlayerColor.Color = brush.Color;

            string path = "/Blockus-Client;component/Resources/Images/ProfileImage-0.png";
            Uri uri = new Uri(path, UriKind.Relative);
            Image_ProfileImage.Source = new BitmapImage(uri);

            AnimationManager.FadeIn(this, .75);
        }

        public void LoadPlayerInformation(MatchMakingServiceClient client, PublicAccountDTO account, BlockusService.Color color, KickerPlayer kicker)
        {
            this.client = client;
            this.account = account;
            this.playerColor = color;
            this.matchPage = kicker;
            txt_Username.Text = account.Username;

            var colorMapping = new Dictionary<BlockusService.Color, string> 
            {
                { BlockusService.Color.Red, "Red" },
                { BlockusService.Color.Blue, "Blue" },
                { BlockusService.Color.Yellow, "Yellow" },
                { BlockusService.Color.Green, "Green" }
            };

            var colorType = colorMapping.ContainsKey(color) ? colorMapping[color] : "Gray";
            SolidColorBrush brush = (SolidColorBrush)Application.Current.Resources[colorType]; 

            Border_Stroke.BorderBrush = brush;
            GradientStop_PlayerColor.Color = brush.Color;

            var imageMapping = new Dictionary<int, string>
            {
                { 1, "/Blockus-Client;component/Resources/Images/ProfileImage-1.png" },
                { 2, "/Blockus-Client;component/Resources/Images/ProfileImage-2.png" }
            };

            if (imageMapping.TryGetValue(account.ProfileImage, out string imagePath))
            {
                Uri uri = new Uri(imagePath, UriKind.Relative);
                Image_ProfileImage.Source = new BitmapImage(uri); 
            }

            AnimationManager.FadeIn(this, .75); 
        }

        private void KickPlayer(object sender, RoutedEventArgs e)
        {
            client.KickPlayer(account.Username);
            matchPage.KickPlayer(playerColor);
        }

        public void showBtnKickPlayer()
        {
            btn_KickPlayer.Visibility = Visibility.Visible;
        }
    }
}
