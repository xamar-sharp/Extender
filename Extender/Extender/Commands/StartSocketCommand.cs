using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Extender.Abstractions;
using Extender.ViewModels;
using Xamarin.Forms;
using System.Net;
using System.Net.Sockets;
namespace Extender.Commands
{
    public class StartSocketCommand : IUpdatableCommand, IDisposable
    {
        public event EventHandler CanExecuteChanged;
        private readonly Socket _socket;
        private readonly SocketRequestViewModel _requestModel;
        private readonly Page _page;
        private readonly ISocketWorker _tcpWorker;
        private readonly ISocketWorker _udpWorker;
        private volatile int _counter = 0;
        public StartSocketCommand(Page page, SocketRequestViewModel viewModel, ISocketWorker tcpWorker, ISocketWorker udpWorker)
        {
            _page = page;
            _requestModel = viewModel;
            _socket = new Socket(AddressFamily.InterNetwork, viewModel.ProtocolType == ProtocolType.Tcp ? SocketType.Stream : SocketType.Dgram, viewModel.ProtocolType);
            _tcpWorker = tcpWorker;
            _udpWorker = udpWorker;
        }
        public void Dispose()
        {
            if (_socket.Connected)
                _socket.Disconnect(false);
            _socket.Close();
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public async void Execute(object arg)
        {
            try
            {
                if (_counter <= 0)
                {
                    _tcpWorker.Bind(_socket, _requestModel.Address, _requestModel.Port);
                    System.Threading.Interlocked.Increment(ref _counter);
                    if (_requestModel.IsServer)
                    {
                        _socket.Listen(ServicePointManager.DefaultConnectionLimit);
                    }
                }
                if (_socket.ProtocolType == ProtocolType.Udp)
                {
                    await _page.Navigation.PushAsync(new ResponseContentPage(await _udpWorker.MakeSession(_socket, _requestModel.Data, new IPEndPoint(_requestModel.RemoteAddress, _requestModel.RemotePort), _requestModel.IsServer)));
                }
                else
                {
                    await _page.Navigation.PushAsync(new ResponseContentPage(await _tcpWorker.MakeSession(_socket, _requestModel.Data, new IPEndPoint(_requestModel.RemoteAddress, _requestModel.RemotePort), _requestModel.IsServer)));
                }
            }
            catch(Exception ex)
            {
                await _page.DisplayAlert(Resource.ErrorTitle, Resource.SocketError, Resource.CancelTitle);
            }
        }
        public bool CanExecute(object arg)
        {
            return _requestModel.Port != 0 && _requestModel.Address != null && _requestModel.Data != null;
        }
    }
}
