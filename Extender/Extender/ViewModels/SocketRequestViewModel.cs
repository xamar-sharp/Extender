using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Extender.Abstractions;
using Extender.Implementations;
using Extender.Commands;
using System.Net;
using System.Net.Sockets;
namespace Extender.ViewModels
{
    public class SocketRequestViewModel : INotifyPropertyChanged
    {
        private IPAddress _address;
        private IPAddress _remoteAddress;
        private ProtocolType _protocolType;
        private short _port;
        private short _remotePort;
        private byte[] _data;
        private bool _isServer;
        private Xamarin.Forms.Page _page;
        public IPAddress Address { get { return _address; } set { _address = value; OnPropertyChanged(); } }
        public IPAddress RemoteAddress { get { return _remoteAddress; } set { _remoteAddress = value; OnPropertyChanged(); } }
        public short RemotePort { get { return _remotePort; } set { _remotePort = value;OnPropertyChanged(); } }
        public bool IsServer { get { return _isServer; } set { _isServer = value;OnPropertyChanged(); } }
        public ProtocolType ProtocolType { get { return _protocolType; } set { _protocolType = value; OnPropertyChanged(); } }
        public short Port { get { return _port; } set { _port = value; OnPropertyChanged(); } }
        public byte[] Data { get { return _data; } set { _data = value; OnPropertyChanged(); } }
        public IUpdatableCommand HostEnteredCommand { get; set; }
        public IUpdatableCommand PortEnteredCommand { get; set; }
        public IUpdatableCommand RemoteHostEnteredCommand { get; set; }
        public IUpdatableCommand RemotePortEnteredCommand { get; set; }
        public IUpdatableCommand SendCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public SocketRequestViewModel(Xamarin.Forms.Page upperPage)
        {
            _page = upperPage;
            SendCommand = new StartSocketCommand(_page, this,new TcpSocketWorker(),new UdpSocketWorker());
            HostEnteredCommand = new HostEnteredCommand(this, false,SendCommand);
            PortEnteredCommand = new PortEnteredCommand(this, false,SendCommand);
            RemoteHostEnteredCommand = new HostEnteredCommand(this, true,SendCommand);
            RemotePortEnteredCommand = new PortEnteredCommand(this, true,SendCommand);
        }
        public void OnPropertyChanged([CallerMemberName] string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));     
        }
    }
}
