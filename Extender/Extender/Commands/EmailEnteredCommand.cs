using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using EmailValidation;
using System.Windows.Input;
using Extender.ViewModels;
namespace Extender.Commands
{
    public class EmailEnteredCommand:ICommand
    {
        private readonly IUpdatableCommand _sendCommand;
        private readonly SmtpRequestViewModel _request;
        public EmailEnteredCommand(SmtpRequestViewModel request,IUpdatableCommand sendCommand)
        {
            _sendCommand = sendCommand;
            _request = request;
        }
        public void Execute(object arg)
        {
            string none = (arg as Xamarin.Forms.Entry).Text;
            if (EmailValidator.Validate(none)) {
                _request.Email = (arg as Xamarin.Forms.Entry).Text;
                _sendCommand.ChangeCanExecute();
            }
        }
        public bool CanExecute(object arg)
        {
            return arg as Xamarin.Forms.Entry != null;
        }
        public event EventHandler CanExecuteChanged;
    }
}
