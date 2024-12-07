using System.Windows.Controls;

namespace Blockus_Client.UserControls
{
    public partial class PlayerMessage : UserControl
    {
        public PlayerMessage(string username, string message)
        {
            InitializeComponent();

            Txt_Message.Text = message;
            Txt_Username.Text = username;
        }
    }
}
