using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Extender.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResponseContentPage : ContentPage,IThemeChanger,ICacheWorker
    {
        public ResponseContentPage(byte[] data)
        {
            InitializeComponent();
            Title = Resource.SocketResponseTitle;
            response.Text = Encoding.Default.GetString(data);
            SetTheme(CheckCache("Theme"));
        }
        public bool CheckCache(string key)
        {
            return Preferences.ContainsKey(key) && Preferences.Get(key, "Day") == "Night";
        }
        public void SetTheme(bool useNightTheme)
        {
            if (useNightTheme || DateTime.Now.Hour >= 18)
            {
                response.SetDynamicResource(StyleProperty, "NightLabel");
            }
            else
            {
                response.SetDynamicResource(StyleProperty, "DayLabel");
            }
        }
    }
}