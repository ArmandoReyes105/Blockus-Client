using Blockus_Client.BlockusService;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Blockus_Client.UserControls
{
    public partial class PunctuationCard : UserControl
    {
        public PunctuationCard()
        {
            InitializeComponent();
        }

        public void SetInformation(string username, int punctuation, Blockus_Client.BlockusService.Color color)
        {
            Txt_Points.Text = punctuation.ToString();
            Txt_Username.Text = username;


            var colorMapping = new Dictionary<BlockusService.Color, string>
            {
                { BlockusService.Color.Red, "Red" },
                { BlockusService.Color.Blue, "Blue" },
                { BlockusService.Color.Yellow, "Yellow" },
                { BlockusService.Color.Green, "Green" }
            };

            var colorType = colorMapping.ContainsKey(color) ? colorMapping[color] : "Gray";
            SolidColorBrush brush = (SolidColorBrush)Application.Current.Resources[colorType];
            GradientStop_PlayerColor.Color = brush.Color;
        }
    }
}
