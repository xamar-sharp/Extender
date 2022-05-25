using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Extender.ViewModels;
using Extender.Abstractions;
using Extender.Implementations;
namespace Extender.Commands
{
    public class SendCommand:IUpdatableCommand
    {
        private readonly Page _page;
        private readonly IHttpRequestBearer _bearer;
        private readonly INetworkWorker _networkWorker;
        public SendCommand(Page page,IHttpRequestBearer bearer)
        {
            _page = page;
            _bearer = bearer;
            _networkWorker = new NetworkWorker();
            CanExecuteChanged = (s, a) =>
            {

            };
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public async void Execute(object arg)
        {
            if (_networkWorker.HasNetworkAccess())
                await _page.Navigation.PushAsync(new HttpResponsePage(await _bearer.HttpRequest.ExecuteAsync()));
            else
                await _page.DisplayAlert(Resource.ErrorTitle, Resource.NoNetworkAccess, Resource.CancelTitle) ;
        }
        public bool CanExecute(object arg)
        {
          return _page != null && _bearer.HttpRequest != null && Uri.IsWellFormedUriString(_bearer.HttpRequest.RequestUri,UriKind.Absolute); 
        }
        public event EventHandler CanExecuteChanged;
    }
}
