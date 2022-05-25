using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extender.ViewModels;
using Extender.Abstractions;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetPage : ContentPage,IThemeChanger,ICacheWorker
    {
        public DownloadViewModelList DownloadList { get; set; }
        public NetPage()
        {
            InitializeComponent();
            IconImageSource = "loading.png";
            Title = Resource.NetPageTitle;
            DownloadList = new DownloadViewModelList();
            search.Placeholder = Resource.NetPagePlaceholder;
            SetTheme(CheckCache("Theme"));
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
                downloads.SetDynamicResource(StyleProperty, "NightListView");
                root.SetDynamicResource(StyleProperty, "NightStackLayout");
                search.SetDynamicResource(StyleProperty, "NightSearchBar");
            }
            else
            {
                downloads.SetDynamicResource(StyleProperty, "DayListView");
                root.SetDynamicResource(StyleProperty, "DayStackLayout");
                search.SetDynamicResource(StyleProperty, "DaySearchBar");
            }
        }
    }
}