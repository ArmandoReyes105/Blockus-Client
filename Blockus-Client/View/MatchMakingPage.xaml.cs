using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class MatchMakingPage : Page, IMatchMakingServiceCallback
    {

        private readonly MatchMakingServiceClient client; 
        private MatchDTO match; 

        public MatchMakingPage()
        {
            InitializeComponent();
            LanguageManager.ApplyCulture();
            AnimationManager.FadeIn(this, .75);
            client = new MatchMakingServiceClient(new InstanceContext(this));

            try
            {
                InitializeMatch();
            }
            catch (Exception ex) 
            {
                HandleError(Properties.Resources.MatchDetails_createError, ex);
            }
            
        }

        private void InitializeMatch()
        {

            var currentAccount = SessionManager.Instance.GetCurrentAccount();

            PublicAccountDTO publicAccount = new PublicAccountDTO
            {
                Id = currentAccount.Id,
                Username = currentAccount.Username,
                ProfileImage = currentAccount.ProfileImage
            };

            match = client.CreateMatch(publicAccount);
            LoadInformation();
            
        }

        private void LeaveMatch(object sender, RoutedEventArgs e)
        {

            try
            {
                var account = SessionManager.Instance.GetCurrentAccount();
                client.LeaveMatchAsync(account.Username);
                client.Close();

                NavigationManager.Instance.NavigateTo(new LobbyPage());
            }
            catch (Exception ex)
            {
                HandleError(Properties.Resources.MatchDetails_errorLeft, ex); 
            }

        }

        private void LoadInformation()
        {
            txt_MatchCode.Text = match.MatchCode;
            txt_Players.Text = match.Players.Count.ToString();
            txt_TotalPlayers.Text = match.NumberOfPlayers.ToString();

            var playerCards = new[] { UC_CardOne, uc_CardTwo, uc_CardThree, uc_CardFour};
            playerCards.ToList().ForEach(p => p.ResetInformation());

            var playerList = match.Players.ToList(); 

            for (int i = 0; i < match.Players.Count; i++)
            {
                var color = playerList[i].Key;
                var player = playerList[i].Value; 
                playerCards[i].LoadPlayerInformation(player, color);
            }

        }

        //Callback Methods
        public void NotifyPlayerEntry(MatchDTO matchDTO)
        {
            match = matchDTO;
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

        //Auxiliar Methods
        private void HandleError(string message, Exception ex)
        {
            Console.WriteLine(ex.ToString());
            MessageBox.Show(Properties.Resources.Error_serverConnection);
            NavigationManager.Instance.NavigateTo(new LoginPage());
            SessionManager.Instance.LogOut();
        }

    } 
}
