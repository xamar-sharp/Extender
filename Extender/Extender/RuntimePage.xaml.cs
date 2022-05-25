using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extender.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RuntimePage : TabbedPage,IThemeChanger,ICacheWorker
    {
        public RuntimePage()
        {
            InitializeComponent();
            SetTheme(CheckCache("Theme"));
            Title = Resource.RuntimeTitle;
        }
        public bool CheckCache(string key)
        {
            return Preferences.ContainsKey(key) && Preferences.Get(key, "Day") == "Night";
        }
        public void SetTheme(bool useNightTheme)
        {
            if (useNightTheme || DateTime.Now.Hour >= 18)
            {
                Shell.SetForegroundColor(this, Device.GetNamedColor(NamedPlatformColor.HoloPurple));
                Shell.SetBackgroundColor(this, Device.GetNamedColor(NamedPlatformColor.Black));
                SetDynamicResource(StyleProperty, "NightTabbedPage");
            }
            else
            {
                Shell.SetForegroundColor(this, Device.GetNamedColor(NamedPlatformColor.White));
                Shell.SetBackgroundColor(this, Device.GetNamedColor(NamedPlatformColor.HoloBlueDark));
                SetDynamicResource(StyleProperty, "DayTabbedPage");

            }
        }
    }
}