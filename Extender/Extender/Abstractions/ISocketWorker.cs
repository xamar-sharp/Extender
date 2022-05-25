using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
namespace Extender.Abstractions
{
    public interface ISocketWorker
    {
        void Bind(Socket socket, IPAddress address, short port);
        ValueTask<byte[]> MakeSession(Socket socket, byte[] data, IPEndPoint remote, bool isReceiver);
    }
}
