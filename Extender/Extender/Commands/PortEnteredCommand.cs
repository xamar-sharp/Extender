using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using Extender.ViewModels;
using Xamarin.Forms;
namespace Extender.Commands
{
    public class PortEnteredCommand:IUpdatableCommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly SocketRequestViewModel _requestModel;
        private readonly bool _isRemote;
        private readonly IUpdatableCommand _sendCommand;

        public PortEnteredCommand(SocketRequestViewModel viewModel,bool isRemote,IUpdatableCommand sendCommand)
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
                if (short.TryParse((arg as Entry).Text, out short result))
                {
                    _requestModel.RemotePort = result;
                }
                else
                {
                    _requestModel.RemotePort = 80;
                }
            }
            else
            {
                if (short.TryParse((arg as Entry).Text, out short result))
                {
                    _requestModel.Port = result;
                }
                else
                {
                    _requestModel.Port = 80;
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
