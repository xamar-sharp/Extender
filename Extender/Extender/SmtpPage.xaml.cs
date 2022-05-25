using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extender.Abstractions;
using Extender.Commands;
using Extender.Implementations;
using Extender.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmtpPage : ContentPage,IThemeChanger,ICacheWorker
    {
        public SmtpRequestViewModel SmtpRequest { get; set; }
        public IUpdatableCommand InitAndSendCommand { get; set; }
        public MailViewModelList Mails { get; set; }
        public SmtpPage()
        {
            InitializeComponent();
            IconImageSource = "gmail.png";
            Title = Resource.MailPageTitle;
            INetworkWorker networker = new NetworkWorker();
            IMailMediator mediator = new MailMediator(new FileSaver());
            SmtpRequest = new SmtpRequestViewModel(this, mediator, networker);
            Mails = new MailViewModelList();
            InitAndSendCommand = new InitAndSendCommand(SmtpRequest, Mails.Mails);
            send.Text = Resource.SendMail;
            email.Placeholder = Resource.EmailPlaceholder;
            password.Placeholder = Resource.PasswordPlaceholder;
            to.Placeholder = Resource.ToPlaceholder;
            addFileAttachment.Text = Resource.AddAttachmentText;
            message.Placeholder = Resource.MessageMailPlaceholder;
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
                background.Source = "mailnight.jpg";
                email.SetDynamicResource(StyleProperty, "NightEntry");
                password.SetDynamicResource(StyleProperty, "NightEntry");
                to.SetDynamicResource(StyleProperty, "NightEntry");   
                root.SetDynamicResource(StyleProperty, "NightAbsoluteLayout");
                addFileAttachment.SetDynamicResource(StyleProperty, "NightButton");  
                message.SetDynamicResource(StyleProperty, "NightEditor");
                attachments.SetDynamicResource(StyleProperty, "NightListView");
                send.SetDynamicResource(StyleProperty, "NightButton");
                
            }
            else
            {
                background.Source = "daymail.jpg";
                email.SetDynamicResource(StyleProperty, "DayEntry");
                password.SetDynamicResource(StyleProperty, "DayEntry");
                to.SetDynamicResource(StyleProperty, "DayEntry");
                root.SetDynamicResource(StyleProperty, "DayAbsoluteLayout");
                addFileAttachment.SetDynamicResource(StyleProperty, "DayButton");
                message.SetDynamicResource(StyleProperty, "DayEditor");
                attachments.SetDynamicResource(StyleProperty, "DayListView");
                send.SetDynamicResource(StyleProperty, "DayButton");
            }
        }
        private void message_Completed(object sender, EventArgs e)
        {
            SmtpRequest.MessageEnteredCommand.Execute(message);
            InitAndSendCommand.ChangeCanExecute();
        }
    }
}