namespace Blockus_Client.Helpers
{
    public static class LanguageManager
    {

        public static void SetLanguageToEnglish()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }

        public static void SetLanguageToSpanish()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
        }
    }
}
