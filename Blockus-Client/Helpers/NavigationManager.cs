using System.Windows.Controls;

namespace Blockus_Client.Helpers
{
    public class NavigationManager
    {
        private static NavigationManager _instance = new NavigationManager();
        private Frame mainFrame;
        private MainWindow mainWindow;
        
        private NavigationManager() { }

        public static NavigationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationManager();
                }

                return _instance;
            }
        }

        public void Initialize(Frame mainFrame, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.mainFrame = mainFrame;
        }

        public void NavigateTo(Page page)
        {
            if (mainFrame != null)
            {
                mainFrame.Navigate(page);
                mainFrame.NavigationService.RemoveBackEntry(); 
            }
        }

    }
}
