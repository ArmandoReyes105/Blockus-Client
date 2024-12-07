using System.Windows.Controls;

namespace Blockus_Client.UserControls
{
    public partial class OwnMessage : UserControl
    {
        public OwnMessage(string username, string message)
        {
            InitializeComponent();

            Txt_Username.Text = username;
            Txt_Message.Text = message;
        }
    }
}
