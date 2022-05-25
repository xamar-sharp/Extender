using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using Extender.ViewModels;
using System.Windows.Input;
using EmailValidation;
using Xamarin.Forms;
using System.Linq;
namespace Extender.Commands
{
    public class ToEnteredCommand : ICommand
    {
        private readonly IUpdatableCommand _sendCommand;
        private readonly SmtpRequestViewModel _request;
        public ToEnteredCommand(SmtpRequestViewModel request, IUpdatableCommand sendCommand)
        {
            _sendCommand = sendCommand;
            _request = request;
        }
        public void Execute(object arg)
        {
            var set = Functions.Split(new char[] { ',' }, (arg as Entry).Text);
            int counter = 0;
            foreach (var email in set) {
                if (EmailValidator.Validate(email))
                {
                    System.Threading.Interlocked.Add(ref counter, 1);
                }
            }
            if (counter == set.Count())
            {
                _request.To = set;
                _sendCommand.ChangeCanExecute();
            }    
        }
        public bool CanExecute(object arg)
        {
            return arg as Entry != null;
        }
        public event EventHandler CanExecuteChanged;
    }
}
