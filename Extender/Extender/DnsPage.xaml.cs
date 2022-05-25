using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Extender.Abstractions;
using Extender.Implementations;
using Xamarin.Essentials;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DnsPage : ContentPage, IThemeChanger, ICacheWorker
    {
        public ObservableCollection<IPAddress> Addresses { get; set; }
        private readonly INetworkWorker _networkWorker;
        public DnsPage()
        {
            InitializeComponent();
            Title = Resource.DnsPageTitle;
            IconImageSource = "dns.png";
            dns.Placeholder = Resource.DnsPlaceholder;
            SetTheme(CheckCache("Theme"));
            Addresses = new ObservableCollection<IPAddress>();
            _networkWorker = new NetworkWorker();
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
                dns.SetDynamicResource(StyleProperty, "NightEntry");
                root.SetDynamicResource(StyleProperty, "NightStackLayout");
            }
            else
            {
                dns.SetDynamicResource(StyleProperty, "DayEntry");
                root.SetDynamicResource(StyleProperty, "DayStackLayout");
            }
        }
        private async void dns_Completed(object sender, EventArgs e)
        {
            if (_networkWorker.HasNetworkAccess())
            {
                Addresses.Clear();
                foreach (var address in (await Dns.GetHostEntryAsync(dns.Text)).AddressList)
                {
                    Addresses.Add(address);
                }
            }
            else
            {
                await DisplayAlert(Resource.ErrorTitle, Resource.NoNetworkAccess, Resource.CancelTitle);
            }
        }
    }
}