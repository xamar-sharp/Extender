using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Extender.Abstractions;
using Xamarin.Essentials;
namespace Extender
{
    public partial class App : Application,IThemeChanger,ICacheWorker
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainMenu();
        }
        public bool CheckCache(string key)
        {
            return Preferences.ContainsKey(key) && Preferences.Get(key, "Day") == "Night";
        }
        public void SetTheme(bool useNightTheme)
        {
            if (useNightTheme || DateTime.Now.Hour >= 18)
            {
                MainPage.SetDynamicResource(VisualElement.StyleProperty, "NightNavigationPage");
            }
            else
            {
                MainPage.SetDynamicResource(VisualElement.StyleProperty, "DayNavigationPage");
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
