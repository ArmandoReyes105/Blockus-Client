using Blockus_Client.BlockusService;
using Blockus_Client.UserControls;
using System;
using System.Windows.Controls;
using System.ServiceModel;
using Blockus_Client.Helpers;
using System.Windows;
using log4net;

namespace Blockus_Client.View
{
    public partial class ChatPage : Page, IChatServiceCallback
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChatPage));
        private readonly ChatServiceClient _chatClient;
        private readonly string _matchCode; 

        public ChatPage(string matchCode)
        {
            InitializeComponent();

            _chatClient = new ChatServiceClient(new InstanceContext(this));
            _matchCode = matchCode;

            InitilizeClient(); 
        }



        public void OnReciveMessage(string username, string message)
        {
            StackPanel_Messages.Children.Add(new PlayerMessage(username, message));
            Scroll_Messages.ScrollToBottom(); 
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string message = TextBox_Message.Text;
            string username = SessionManager.Instance.GetUsername(); 

            if (!String.IsNullOrEmpty(message))
            {

                try
                {
                    _chatClient.SendMessage(username, _matchCode, message);
                }
                catch (Exception ex) 
                {
                    HandleError(Properties.Resources.Error_serverConnection);
                    log.Error(ex.Message);
                }

                StackPanel_Messages.Children.Add(new OwnMessage(username, message));
                Scroll_Messages.ScrollToBottom();
            }
        }

        private void InitilizeClient()
        {
            try
            {
                var username = SessionManager.Instance.GetUsername();
                _chatClient.JoinToChat(username); 
            }
            catch (Exception ex) 
            {
                HandleError(Properties.Resources.Error_serverConnection);
                log.Error(ex.Message);
            }
        }

        private void HandleError(string message)
        {
            MessageBox.Show(message);
            SessionManager.Instance.LogOut();
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }
    }
}
