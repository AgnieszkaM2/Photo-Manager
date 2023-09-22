using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo_Manager.AdditionalResources
{
    public class SystemThemeSettings
    {
        private static bool IsLightTheme()
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");
            return value is int i && i > 0;
        }

        public static void CheckTheme()
        {
            bool isUserThemeLight = IsLightTheme();
            if (isUserThemeLight == true) 
            {
                AppTheme.ChangeTheme(new Uri("Assets/LightTheme.xaml", UriKind.Relative));
                CurrentResources.CurrentTheme = "Light";
            }
            else
            {
                AppTheme.ChangeTheme(new Uri("Assets/DarkTheme.xaml", UriKind.Relative));
                CurrentResources.CurrentTheme = "Dark";
            }
        }
    }
}
