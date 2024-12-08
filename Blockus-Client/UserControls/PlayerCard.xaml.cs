using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Blockus_Client.UserControls
{
    public partial class PlayerCard : UserControl
    {
        public PlayerCard()
        {
            InitializeComponent();
            LanguageManager.ApplyCulture();
        }

        public void ResetInformation()
        {
            txt_Username.Text = "Esperando jugador ...";

            SolidColorBrush brush = (SolidColorBrush)Application.Current.Resources["Gray"];

            Border_Stroke.BorderBrush = brush;
            GradientStop_PlayerColor.Color = brush.Color;

            string path = "/Blockus-Client;component/Resources/Images/ProfileImage-0.png";
            Uri uri = new Uri(path, UriKind.Relative);
            Image_ProfileImage.Source = new BitmapImage(uri);

            AnimationManager.FadeIn(this, .75);
        }

        public void LoadPlayerInformation(PublicAccountDTO account, BlockusService.Color color)
        {
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
    }
}
