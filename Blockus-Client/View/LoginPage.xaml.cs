using Blockus_Client.BlockusService;
using Blockus_Client.Helpers;
using log4net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Blockus_Client.View
{
    public partial class LoginPage : Page
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(LoginPage));

        public LoginPage()
        {
            InitializeComponent();

            AnimationManager.FadeIn(this, .75);
            AnimationManager.RotateImage(imageRotation, 12);
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            if (AreFieldsComplete())
            {
                string password = HashManager.HashPassword(txt_Password.Password);
                AccountDTO account;

                try
                {
                    var service = new SessionServiceClient();
                    account = await service.LogInAsync(txt_Username.Text, password);
                    service.Close(); 
                }
                catch (CommunicationException ex)
                {
                    log.Error("Error-LogIn: " +  ex.Message);
                    account = null;
                }

                if (account == null)
                {
                    MessageBox.Show(Properties.Resources.Error_serverConnection);
                    return;
                }

                if (account.Id == -1)
                {
                    MessageBox.Show(Properties.Resources.Error_retrievingData);
                    return;
                }

                if (account.Id == 0)
                {
                    MessageBox.Show(Properties.Resources.Error_wrongCredentials);
                    return;
                }

                if (account.Id == -2)
                {
                    MessageBox.Show(Properties.Resources.Error_alreadyLogged);
                    return;
                }

                SessionManager.Instance.LogIn(account);
                NavigationManager.Instance.NavigateTo(new LobbyPage());
            }
        }

        private void LoginAsGuest(object sender, RoutedEventArgs e)
        {
            var guestAccount = new AccountDTO
            {
                Id = 0,
                Username = RandomManager.GenerateRandomName(),
                Email = string.Empty,
                ProfileImage = 1,
                Password = string.Empty
            };

            SessionManager.Instance.LogIn(guestAccount);
            SessionManager.Instance.MakeUserAGuest(); 
            NavigationManager.Instance.NavigateTo(new LobbyPage());
        }

        private bool AreFieldsComplete()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txt_Username.Text) || string.IsNullOrEmpty(txt_Password.Password))
            {
                MessageBox.Show(Properties.Resources.Error_incompleteFields);
                result = false;
            }

            return result;
        }

        private void GoToNewAccountPage(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instance.NavigateTo(new NewAccountPage());
        }

        private void SetLanguageToEnglish(object sender, RoutedEventArgs e)
        {
            LanguageManager.SetLanguageToEnglish();
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }

        private void SetLanguageToSpanish(object sender, RoutedEventArgs e)
        {
            LanguageManager.SetLanguageToSpanish(); 
            NavigationManager.Instance.NavigateTo(new LoginPage());
        }
    }
}