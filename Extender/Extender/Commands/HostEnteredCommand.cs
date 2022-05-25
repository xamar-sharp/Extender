using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Extender.Abstractions;
using Xamarin.Forms;
using Extender.ViewModels;
using System.Net;
namespace Extender.Commands
{
    public class HostEnteredCommand:IUpdatableCommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly SocketRequestViewModel _requestModel;
        private readonly bool _isRemote;
        private readonly IUpdatableCommand _sendCommand;
        public HostEnteredCommand(SocketRequestViewModel viewModel,bool isRemote,IUpdatableCommand sendCommand)
        {
            _isRemote = isRemote;
            _requestModel = viewModel;
            _sendCommand = sendCommand;
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object arg)
        {
            if (_isRemote)
            {
                if (IPAddress.TryParse((arg as Entry).Text, out IPAddress address))
                {
                    _requestModel.RemoteAddress = address;
                }
                else
                {
                    _requestModel.RemoteAddress = IPAddress.Loopback;
                }
            }
            else
            {
                if (IPAddress.TryParse((arg as Entry).Text, out IPAddress address))
                {
                    _requestModel.Address = address;
                }
                else
                {
                    _requestModel.Address = IPAddress.Loopback;
                }
            }
            _sendCommand.ChangeCanExecute();
        }
        public bool CanExecute(object arg)
        {
            return arg as Entry != null;
        }
    }
}
