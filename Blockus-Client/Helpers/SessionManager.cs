using Blockus_Client.BlockusService;
using log4net;
using System.ServiceModel;


namespace Blockus_Client.Helpers
{
    public class SessionManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SessionManager));
        private static SessionManager _instance;
        private static AccountDTO currentAccount = null;
        private static bool _isAGuest = false;
        private static int _tilesConfiguration = 1; 

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
                try
                {
                    using (var client = new SessionServiceClient())
                    {
                        client.LogOut(currentAccount.Username);
                    }
                }
                catch (CommunicationException ex)
                {
                    log.Error("LogOut: " + ex.ToString());
                }
                finally
                {
                    _isAGuest = false;
                    currentAccount = null;
                }
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

        public void MakeUserAGuest()
        {
            _isAGuest = true; 
        }

        public bool IsAGuest() => _isAGuest;

        public void SetTilesConfiguration(int tiles) => _tilesConfiguration = tiles;

        public int GetTilesConfiguration() => _tilesConfiguration;
    }
}
