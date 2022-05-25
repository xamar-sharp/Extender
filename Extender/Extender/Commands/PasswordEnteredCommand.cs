using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using Extender.ViewModels;
using System.Windows.Input;
namespace Extender.Commands
{
    public class PasswordEnteredCommand : ICommand
    {
        private readonly IUpdatableCommand _sendCommand;
        private readonly SmtpRequestViewModel _request;
        public PasswordEnteredCommand(SmtpRequestViewModel request, IUpdatableCommand sendCommand)
        {
            _sendCommand = sendCommand;
            _request = request;
        }
        public void Execute(object arg)
        {
            _request.Password = (arg as Xamarin.Forms.Entry).Text;
            _sendCommand.ChangeCanExecute();
        }
        public bool CanExecute(object arg)
        {
            return arg as Xamarin.Forms.Entry != null;
        }
        public event EventHandler CanExecuteChanged;
    }
}
