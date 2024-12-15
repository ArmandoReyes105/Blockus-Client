using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class JoinMatchPage : Page, IMatchMakingServiceCallback
    {

        private MatchMakingServiceClient client;
        private MatchDTO match; 

        public JoinMatchPage()
        {
            InitializeComponent();
            client = new MatchMakingServiceClient(new InstanceContext(this)); 
        }

        private void JoinToMatch(object sender, RoutedEventArgs e)
        {

            var currentAccount = SessionManager.Instance.GetCurrentAccount();

            PublicAccountDTO publicAccount = new PublicAccountDTO
            {
                Id = currentAccount.Id,
                Username = currentAccount.Username,
                ProfileImage = currentAccount.ProfileImage
            };

            try
            {
                match = client.JoinToMatch(publicAccount, TextBox_Code.Text);

                if (CanJoinToMatch(match.Host))
                {
                    DisplayMatchDetails(); 
                }

            }catch(Exception ex)
            {
                HandleError(Properties.Resources.JoinMatch_unableToJoin, ex); 
            }

        }

        private void LeaveMatch(object sender, RoutedEventArgs e)
        {

            try
            {
                client.LeaveMatchAsync(SessionManager.Instance.GetUsername());
                client.Close();
                NavigationManager.Instance.NavigateTo(new LobbyPage()); 
            }
            catch (Exception ex)
            {
                HandleError("", ex); 
            }

        }

        private void LoadInformation()
        {
            txt_Players.Text = match.Players.Count.ToString();
            txt_TotalPlayers.Text = match.NumberOfPlayers.ToString();

            var playerCards = new[] { UC_CardOne, uc_CardTwo, uc_CardThree, uc_CardFour };
            playerCards.ToList().ForEach(p => p.ResetInformation());
            var playerList = match.Players.ToList();

            for (int i = 0; i < match.Players.Count; i++)
            {
                var color = playerList[i].Key;
                var player = playerList[i].Value;
                playerCards[i].LoadPlayerInformation(player, color);
            }
        }

        private void DisplayMatchDetails()
        {
            StackPanel_Code.Visibility = Visibility.Collapsed;
            Grid_Players.Visibility = Visibility.Visible;
            LoadInformation();

            if (match.NumberOfPlayers == match.Players.Count)
            {
                NavigationManager.Instance.NavigateTo(new GamePage()); 
            }
        }

        private bool CanJoinToMatch(string hostStatus)
        {

            bool result = true;
            string message = null;

            switch (hostStatus)
            {
                case "0": 
                    message = Properties.Resources.JoinMatch_noMatches; 
                    break; 
                case "-1": 
                    message = Properties.Resources.JoinMatch_fullMatch;
                    break; ; 
                default: 
                    break; 
            }

            if (message != null) 
            { 
                MessageBox.Show(message);
                result = false; 
            } 

            return result; 
        }

        //Callback Methods
        public void NotifyPlayerEntry(MatchDTO matchDTO)
        {
            this.match = matchDTO;
            LoadInformation();

            if (match.NumberOfPlayers == match.Players.Count)
            {
                NavigationManager.Instance.NavigateTo(new GamePage());
            }
        }

        public void NotifyPlayerExit(MatchDTO matchDTO)
        {
            this.match = matchDTO;
            LoadInformation();
        }

        public void NotifyHostExit(MatchDTO matchDTO)
        {
            MessageBox.Show(Properties.Resources.JoinMatch_hostLeft);
            NavigationManager.Instance.NavigateTo(new LobbyPage()); 
        }

        //Auxiliar methods
        private void HandleError(string message, Exception ex)
        {
            Console.WriteLine(ex.ToString());
            MessageBox.Show(Properties.Resources.Error_serverConnection);
            NavigationManager.Instance.NavigateTo(new LoginPage());
            SessionManager.Instance.LogOut();
        }
    }
}
