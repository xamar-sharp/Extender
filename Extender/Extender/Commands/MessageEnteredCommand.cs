using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Extender.Abstractions;
using Extender.ViewModels;
using System.Windows.Input;
namespace Extender.Commands
{
    public class MessageEnteredCommand : ICommand
    {
        private readonly IUpdatableCommand _sendCommand;
        private readonly SmtpRequestViewModel _request;
        public MessageEnteredCommand(SmtpRequestViewModel request, IUpdatableCommand sendCommand)
        {
            _sendCommand = sendCommand;
            _request = request;
        }
        public void Execute(object arg)
        {
            _request.Message = (arg as Editor).Text;
            _sendCommand.ChangeCanExecute();
        }
        public bool CanExecute(object arg)
        {
            return arg as Editor != null;
        }
        public event EventHandler CanExecuteChanged;
    }
}
