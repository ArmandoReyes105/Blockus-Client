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
    public partial class ActivePlayerCard : UserControl
    {
        private int _points = 0; 

        public ActivePlayerCard()
        {
            InitializeComponent();
        }

        public void LoadPlayerInformation(PublicAccountDTO account, BlockusService.Color color)
        {
            Txt_Username.Text = account.Username;

            LoadElementsWithColor(color);
            LoadProfileImage(account);
            LoadBackgroundImage(color);
        }

        private void LoadElementsWithColor(BlockusService.Color color)
        {
            var colorMapping = new Dictionary<BlockusService.Color, string>
            {
                { BlockusService.Color.Red, "Red" },
                { BlockusService.Color.Blue, "Blue" },
                { BlockusService.Color.Yellow, "Yellow" },
                { BlockusService.Color.Green, "Green" }
            };

            var colorType = colorMapping.ContainsKey(color) ? colorMapping[color] : "Gray";
            SolidColorBrush brush = (SolidColorBrush)Application.Current.Resources[colorType];

            Border_Image.BorderBrush = brush;
            Border_General.BorderBrush = brush;
        }

        private void LoadProfileImage(PublicAccountDTO account)
        {
            var imageMapping = new Dictionary<int, string>
            {
                { 1, "/Blockus-Client;component/Resources/Images/ProfileImage-1.png" },
                { 2, "/Blockus-Client;component/Resources/Images/ProfileImage-2.png" }
            };

            if (imageMapping.TryGetValue(account.ProfileImage, out string imagePath))
            {
                Uri uri = new Uri(imagePath, UriKind.Relative);
                Image_Profile.Source = new BitmapImage(uri);
            }
        }

        private void LoadBackgroundImage(BlockusService.Color color)
        {
            var imageMapping = new Dictionary<BlockusService.Color, string>
            {
                { BlockusService.Color.Red, "/Blockus-Client;component/Resources/Images/BackgroundCard-Red.png" },
                { BlockusService.Color.Blue, "/Blockus-Client;component/Resources/Images/BackgroundCard-Blue.png" },
                { BlockusService.Color.Yellow, "/Blockus-Client;component/Resources/Images/BackgroundCard-Yellow.png" },
                { BlockusService.Color.Green, "/Blockus-Client;component/Resources/Images/BackgroundCard-Green.png" }
            };

            if (imageMapping.TryGetValue(color, out string imagePath))
            {
                Uri uri = new Uri(imagePath, UriKind.Relative);
                Image_Background.Source = new BitmapImage(uri);
            }
        }

        public void AddPoints(int points)
        {
            _points += points;
            Txt_Points.Text = _points.ToString(); 
        }
    }
}
