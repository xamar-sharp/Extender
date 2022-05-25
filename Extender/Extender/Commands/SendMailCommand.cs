using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using Extender.Implementations;
using Extender.ViewModels;
using System.Linq;
namespace Extender.Commands
{
    public class SendMailCommand:IUpdatableCommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly SmtpRequestViewModel _smtpRequest;
        private readonly IMailMediator _mailMediator;
        private readonly INetworkWorker _networkWorker;
        private readonly Xamarin.Forms.Page _navigablePage;
        public SendMailCommand(Xamarin.Forms.Page navigablePage,SmtpRequestViewModel viewModel,IMailMediator mailMediator,INetworkWorker networkWorker)
        {
            _navigablePage = navigablePage;
            _networkWorker = networkWorker;
            _mailMediator = mailMediator;
            _smtpRequest = viewModel;
        }
        public async void Execute(object arg)
        {
            if (_networkWorker.HasNetworkAccess())
            {
                
                _mailMediator.Authenticate(_smtpRequest.Email, _smtpRequest.Password);
                await _navigablePage.DisplayAlert(Resource.MailSended,await _mailMediator.SendMail(_smtpRequest.Message, _smtpRequest.To, _smtpRequest.Attachments)?Resource.SuccessMail:Resource.FailedMail,Resource.CancelTitle);
            }
            else
            {
                await _navigablePage.DisplayAlert(Resource.ErrorTitle, Resource.NoNetworkAccess, Resource.CancelTitle);
            }
        }
        public bool CanExecute(object arg)
        {
            return _smtpRequest.Email != null && _smtpRequest.To != null && _smtpRequest.To.First() != null && _smtpRequest.Password != null && _smtpRequest.Message != null;
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this,EventArgs.Empty);
        }
    }
}
