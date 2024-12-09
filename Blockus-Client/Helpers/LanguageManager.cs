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

        public static void ApplyCulture()
        {
        }

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
