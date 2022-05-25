using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extender.ViewModels;
using Extender.Abstractions;
using Xamarin.Essentials;
using Extender.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using Extender.Implementations;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HttpCrud : ContentPage,IThemeChanger,ICacheWorker,IHttpRequestBearer
    {
        public HttpRequestViewModel HttpRequest { get; set; }
        public IUpdatableCommand SendCommand { get; set; }

        public HttpCrud()
        {
            InitializeComponent();
            Title = Resource.HttpCrudTitle;
            IconImageSource = "http.png";
            httpMethod.Text = Resource.GetTitle;
            sendModel.Text = Resource.SendModel;
            forPostOrPutFile.Text = Resource.SelectFile;
            forPostOrPut.Placeholder = Resource.JsonPlaceholder;
            authToken.Placeholder = Resource.TokenPlaceholder;
            requestUri.Placeholder = Resource.UriPlaceholder;
            SendCommand = new SendCommand(this, this);
            HttpRequest = new HttpRequestViewModel(SendCommand);
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
                requestUri.SetDynamicResource(StyleProperty, "NightEntry");
                root.SetDynamicResource(StyleProperty, "NightStackLayout");
                httpMethod.SetDynamicResource(StyleProperty, "NightButton");
                useAuth.SetDynamicResource(StyleProperty, "NightCheckBox");
                forPostOrPut.SetDynamicResource(StyleProperty, "NightEntry");
                forPostOrPutFile.SetDynamicResource(StyleProperty, "NightButton");
                sendModel.SetDynamicResource(StyleProperty, "NightButton");
            }
            else
            {
                requestUri.SetDynamicResource(StyleProperty, "DayEntry");
                root.SetDynamicResource(StyleProperty, "DayStackLayout");
                httpMethod.SetDynamicResource(StyleProperty, "DayButton");
                useAuth.SetDynamicResource(StyleProperty, "DayCheckBox");
                forPostOrPut.SetDynamicResource(StyleProperty, "DayEntry");
                forPostOrPutFile.SetDynamicResource(StyleProperty, "DayButton");
                sendModel.SetDynamicResource(StyleProperty, "DayButton");
            }
        }

        private void requestUri_Completed(object sender, EventArgs e)
        {
            HttpRequest.UriEnteredCommand.ChangeCanExecute();
        }
    }
}