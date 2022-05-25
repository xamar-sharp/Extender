using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using Extender.Commands;
using Extender.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
namespace Extender.Commands
{
    public class InitAndSendCommand:IUpdatableCommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly SmtpRequestViewModel _request; 
        private ObservableCollection<MailViewModel> _mails;
        public InitAndSendCommand(SmtpRequestViewModel request,ObservableCollection<MailViewModel> mails)
        {
            _request = request;
            _mails = mails;
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public bool CanExecute(object arg)
        {
            return _request.SendCommand.CanExecute(arg);
        }
        public void Execute(object arg)
        {
            _request.Attachments = _mails.AsParallel().AsOrdered().Select(model => model.PhysicalPath);
            _request.SendCommand.Execute(arg);
        }
    }
}
