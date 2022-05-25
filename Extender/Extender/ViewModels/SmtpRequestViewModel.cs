using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Extender.Commands;
using Extender.Abstractions;
using Extender.Implementations;
using System.Windows.Input;
namespace Extender.ViewModels
{
    public class SmtpRequestViewModel:INotifyPropertyChanged
    {
        private string _email, _message,_password;
        private IEnumerable<string> _to, _attachments;
        public string Email { get { return _email; } set { _email = value;OnPropertyChanged(); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public IEnumerable<string> To { get { return _to; } set { _to = value; OnPropertyChanged(); } }
        public IEnumerable<string> Attachments { get { return _attachments; } set { _attachments = value; OnPropertyChanged(); } }
        public string Message { get { return _message; } set { _message = value; OnPropertyChanged(); } }
        public IUpdatableCommand SendCommand { get; set; }
        public ICommand ToEnteredCommand { get; set; }
        public ICommand EmailEnteredCommand { get; set; }
        public ICommand PasswordEnteredCommand { get; set; }
        public ICommand MessageEnteredCommand { get; set; }
        public SmtpRequestViewModel(Xamarin.Forms.Page page,IMailMediator mediator,INetworkWorker worker)
        {
            SendCommand = new SendMailCommand(page, this, mediator, worker);
            ToEnteredCommand = new ToEnteredCommand(this, SendCommand);
            EmailEnteredCommand = new EmailEnteredCommand(this, SendCommand);
            PasswordEnteredCommand = new PasswordEnteredCommand(this, SendCommand);
            MessageEnteredCommand = new MessageEnteredCommand(this, SendCommand);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
