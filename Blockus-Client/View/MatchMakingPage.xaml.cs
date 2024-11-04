using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class MatchMakingPage : Page
    {

        private MatchDTO match; 

        public MatchMakingPage()
        {
            InitializeComponent();

            InitializeMatch();
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

            MatchMakingServiceClient client = new MatchMakingServiceClient();
            match = client.CreateMatch(publicAccount);
            client.Close();

            LoadInformation(); 
        }

        private void LoadInformation()
        {
            txt_MatchCode.Text = match.MatchCode;
            txt_Players.Text = match.Players.Length.ToString();
            txt_TotalPlayers.Text = match.NumberOfPlayers.ToString();

            var playerCards = new[] { UC_CardOne, uc_CardTwo, uc_CardThree, uc_CardFour};

            for (int i = 0; i < match.Players.Length; i++)
            {
                playerCards[i].LoadPlayerInformation(match.Players[i], match.ColorsOrder[i]);
            }
        }
    }
}
