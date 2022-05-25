using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Threading;
using Extender.Commands;
using Extender.Abstractions;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HttpResponsePage : ContentPage,IThemeChanger,ICacheWorker
    {
        public HttpResponseMessage Response { get; set; }
        public ICommand ClipboardSaveCommand { get; set; }
        private readonly SynchronizationContext _sync;
        public HttpResponsePage(HttpResponseMessage message)
        {
            InitializeComponent();
            Title = Resource.HttpResponseTitle;
            if(message is null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert(Resource.ErrorTitle, Resource.HttpResponseError, Resource.CancelTitle);
                    await Navigation.PopAsync();
                });
                return;
            }           
            SetTheme(CheckCache("Theme"));
            Response = message;
            _sync = SynchronizationContext.Current;
            headerLabel.Text = JsonConvert.SerializeObject(message.Headers);
            _sync.Post(async (obj) =>
            {
                responseLabel.Text = await Response.Content.ReadAsStringAsync();
            }, 1);
            saveClipboard.Text = Resource.SaveClipboard;
            ClipboardSaveCommand = new ClipboardSaveCommand();
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
                separator.SetDynamicResource(StyleProperty, "NightBoxView");
                rootScroll.SetDynamicResource(StyleProperty, "NightScrollView");
                statusLabel.SetDynamicResource(StyleProperty, "NightLabel");
                root.SetDynamicResource(StyleProperty, "NightStackLayout");
                descriptionImage.SetDynamicResource(StyleProperty, "NightImage");
                descriptionScroll.SetDynamicResource(StyleProperty, "NightScrollView");
                responseLabel.SetDynamicResource(StyleProperty, "NightEditor");
                headerLabel.SetDynamicResource(StyleProperty, "NightEditor");
                headersScroll.SetDynamicResource(StyleProperty, "NightScrollView");
                saveClipboard.SetDynamicResource(StyleProperty, "NightButton");
            }
            else
            {
                separator.SetDynamicResource(StyleProperty, "DayBoxView");
                rootScroll.SetDynamicResource(StyleProperty, "DayScrollView");
                statusLabel.SetDynamicResource(StyleProperty, "DayLabel");
                root.SetDynamicResource(StyleProperty, "DayStackLayout");
                descriptionImage.SetDynamicResource(StyleProperty, "DayImage");
                descriptionScroll.SetDynamicResource(StyleProperty, "DayScrollView");
                responseLabel.SetDynamicResource(StyleProperty, "DayEditor");
                headerLabel.SetDynamicResource(StyleProperty, "DayEditor");
                headersScroll.SetDynamicResource(StyleProperty, "DayScrollView");
                saveClipboard.SetDynamicResource(StyleProperty, "DayButton");
            }
        }
    }
}