using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blockus_Client.Helpers
{
    public static class LanguageManager
    {
        private static string _currentLanguage = "es-MX";

        public static string CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                _currentLanguage = value;
                ApplyCulture();
            }
        }

        public static void ApplyCulture()
        {
            var language = new CultureInfo(_currentLanguage);
            Thread.CurrentThread.CurrentCulture = language;
            Thread.CurrentThread.CurrentUICulture = language;

            Properties.Resources.Culture = _currentLanguage == "es-MX" ? language : null;
        }
    }
}
