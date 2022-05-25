using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Extender.Abstractions;
using static Extender.Mappings;
using Extender.Implementations;
using Extender.Models;
using System.Windows.Input;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProcessStartPage : ContentPage,IThemeChanger,ICacheWorker
    {
        private readonly ProcessManipulator _processManipulator;
        private readonly StartProcessModel _startModel;
        public ICommand StartAndPopCommand { get; set; }
        public ProcessStartPage(ProcessManipulator manipulator)
        {
            InitializeComponent();
            _startModel = new StartProcessModel();
            _processManipulator = manipulator;
            SetTheme(CheckCache("Theme"));
            StartAndPopCommand = new Command(async () =>
              {
                  _processManipulator.Start(_startModel.FilePath, WindowStyles[_startModel.WindowStyle], _startModel.Arguments, _startModel.Password, _startModel.UserName);
                  await Navigation.PopAsync();
              });
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
                startProcess.Text = Resource.ProcessStartButton;
                startProcess.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button));
                tableView.SetDynamicResource(StyleProperty, "NightTableView");
                startProcess.SetDynamicResource(StyleProperty, "NightButton");
                root.Title = Resource.ProcessStart;
                processFileName.Label = Resource.FileNameTitle;
                processFileName.Placeholder = Resource.FileNamePlaceholder;
                processFileName.LabelColor = Device.GetNamedColor(NamedPlatformColor.White);
                processArguments.Label = Resource.ArgumentListTitle;
                processArguments.Placeholder = Resource.ArgumentListPlaceholder;
                processArguments.LabelColor = Device.GetNamedColor(NamedPlatformColor.White);
                processInfo.Title = Resource.ProcessInfo;
                processWindowStyle.Label = Resource.WindowStyleTitle;
                processWindowStyle.Placeholder = Resource.WindowStylePlaceholder;
                processWindowStyle.LabelColor = Device.GetNamedColor(NamedPlatformColor.White);
                passwordUsername.Label = Resource.CredentialsTitle;
                passwordUsername.Placeholder = Resource.PasswordUserNamePlaceholder;
                passwordUsername.LabelColor = Device.GetNamedColor(NamedPlatformColor.White);
                useAuth.Text = Resource.UseCredentials;
            }
            else
            {
                startProcess.Text = Resource.ProcessStartButton;
                startProcess.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button));
                tableView.SetDynamicResource(StyleProperty, "DayTableView");
                startProcess.SetDynamicResource(StyleProperty, "DayButton");
                root.Title = Resource.ProcessStart;
                processFileName.Label = Resource.FileNameTitle;
                processFileName.Placeholder = Resource.FileNamePlaceholder;
                processFileName.LabelColor = Device.GetNamedColor(NamedPlatformColor.Black);
                processArguments.Label = Resource.ArgumentListTitle;
                processArguments.Placeholder = Resource.ArgumentListPlaceholder;
                processArguments.LabelColor = Device.GetNamedColor(NamedPlatformColor.Black);
                processInfo.Title = Resource.ProcessInfo;
                processWindowStyle.Label = Resource.WindowStyleTitle;
                processWindowStyle.Placeholder = Resource.WindowStylePlaceholder;
                processWindowStyle.LabelColor = Device.GetNamedColor(NamedPlatformColor.Black);
                passwordUsername.Label = Resource.CredentialsTitle;
                passwordUsername.Placeholder = Resource.PasswordUserNamePlaceholder;
                passwordUsername.LabelColor = Device.GetNamedColor(NamedPlatformColor.Black);
                useAuth.Text = Resource.UseCredentials;
            }
        }
        private void processFileName_Completed(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(processFileName.Text))
            {
                _startModel.FilePath = processFileName.Text;
            }
            else
            {
                var drives = System.IO.DriveInfo.GetDrives();
                _startModel.FilePath = drives.FirstOrDefault().RootDirectory.GetFiles("", System.IO.SearchOption.AllDirectories).FirstOrDefault(file => file.Extension == ".exe").FullName;
            }
        }

        private void processArguments_Completed(object sender, EventArgs e)
        {
            _startModel.Arguments = Functions.Split(new char[] { ';' }, processArguments.Text).ToList();
        }

        private void processWindowStyle_Completed(object sender, EventArgs e)
        {
            if (WindowStyles.ContainsKey(processWindowStyle.Text))
            {
                _startModel.WindowStyle = processWindowStyle.Text;
            }
            else
            {
                _startModel.WindowStyle = "DEFAULT";
            }
        }

        private void passwordUsername_Completed(object sender, EventArgs e)
        {
            var list = Functions.Split(new char[] { ';' }, passwordUsername.Text).ToList();
            _startModel.Password = list[0];
            _startModel.UserName = list[1];
        }
        private void useAuth_OnChanged(object sender, ToggledEventArgs e)
        {

        }

    }
}