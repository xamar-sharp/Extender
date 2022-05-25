using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Extender.Abstractions;
using Extender.Implementations;
using Extender.ViewModels;
using Xamarin.Essentials;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocketPage : ContentPage,IThemeChanger,ICacheWorker
    {
        public SocketRequestViewModel SocketRequest { get; set; }
        public SocketPage()
        {
            InitializeComponent();
            IconImageSource = "tcpicon.png";
            Title = Resource.SocketPageTitle;
            SocketRequest = new SocketRequestViewModel(this);
            host.Placeholder = Resource.HostPlaceholder;
            port.Placeholder = Resource.PortPlaceholder;
            remotehost.Placeholder = Resource.RemoteHostPlaceholder;
            remoteport.Placeholder = Resource.RemotePortPlaceholder;
            tcp.Content = Resource.Tcp;
            udp.Content = Resource.Udp;
            connect.Text = Resource.SocketStartTitle;
            content.Placeholder = Resource.SocketDataPlaceholder;
            isServer.Text = Resource.IsServerTitle;
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
                host.SetDynamicResource(StyleProperty, "NightEntry");
                remoteport.SetDynamicResource(StyleProperty, "NightEntry");
                port.SetDynamicResource(StyleProperty, "NightEntry");
                remotehost.SetDynamicResource(StyleProperty, "NightEntry");
                root.SetDynamicResource(StyleProperty, "NightStackLayout");
                tcp.SetDynamicResource(StyleProperty, "NightRadioButton");
                udp.SetDynamicResource(StyleProperty, "NightRadioButton");
                isServerStack.SetDynamicResource(StyleProperty, "NightStackLayout");
                isServer.SetDynamicResource(StyleProperty, "NightLabel");
                isServerCheckBox.SetDynamicResource(StyleProperty, "NightCheckBox");
                connect.SetDynamicResource(StyleProperty, "NightButton");
                content.SetDynamicResource(StyleProperty, "NightEditor");
            }
            else
            {
                host.SetDynamicResource(StyleProperty, "DayEntry");
                remoteport.SetDynamicResource(StyleProperty, "DayEntry");
                port.SetDynamicResource(StyleProperty, "DayEntry");
                remotehost.SetDynamicResource(StyleProperty, "DayEntry");
                root.SetDynamicResource(StyleProperty, "DayStackLayout");
                tcp.SetDynamicResource(StyleProperty, "DayRadioButton");
                udp.SetDynamicResource(StyleProperty, "DayRadioButton");
                isServerStack.SetDynamicResource(StyleProperty, "DayStackLayout");
                isServer.SetDynamicResource(StyleProperty, "DayLabel");
                isServerCheckBox.SetDynamicResource(StyleProperty, "DayCheckBox");
                connect.SetDynamicResource(StyleProperty, "DayButton");
                content.SetDynamicResource(StyleProperty, "DayEditor");
            }
        }
        private void tcp_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                SocketRequest.ProtocolType = System.Net.Sockets.ProtocolType.Tcp;
            }
        }

        private void udp_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                SocketRequest.ProtocolType = System.Net.Sockets.ProtocolType.Udp;
            }
        }

        private void isServerCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                SocketRequest.IsServer = true;
            }
            else
            {
                SocketRequest.IsServer = false;
            }
        }

        private void content_Completed(object sender, EventArgs e)
        {
            SocketRequest.Data = Encoding.Default.GetBytes(content.Text);
            SocketRequest.SendCommand.ChangeCanExecute();
        }
    }
}