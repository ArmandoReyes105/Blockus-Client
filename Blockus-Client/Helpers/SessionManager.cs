using Blockus_Client.BlockusService;


namespace Blockus_Client.Helpers
{
    public class SessionManager
    {
        private static SessionManager _instance;
        private static AccountDTO currentAccount = null; 

        private SessionManager() { }

        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionManager ();
                }

                return _instance; 
            }
        }

        public void LogIn(AccountDTO account)
        {
            currentAccount = account; 
        }

        public void LogOut()
        {
            if (currentAccount != null) 
            {
                SessionServiceClient client = new SessionServiceClient();
                client.LogOut(currentAccount.Username);
                client.Close();

                currentAccount = null;
            }
        }

        public AccountDTO GetCurrentAccount()
        {
            return currentAccount;
        }

        public string GetUsername()
        {
            return currentAccount.Username;
        }
    }
}
