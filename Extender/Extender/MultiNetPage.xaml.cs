using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;
using Extender.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiNetPage:TabbedPage,IThemeChanger,ICacheWorker
    {
        public MultiNetPage()
        {
            InitializeComponent();
            SetTheme(CheckCache("Theme"));
            Title = Resource.MultiNetTitle;
            this.BindingContext = this;
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
