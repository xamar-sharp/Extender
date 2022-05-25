using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace Extender.Implementations
{
    public readonly struct UdpSocketWorker : ISocketWorker
    {
        public void Bind(Socket socket, IPAddress address, short port)
        {
            socket.Bind(new IPEndPoint(address, port));
        }
        async ValueTask<byte[]> makeServerSession(Socket socket, byte[] data, IPEndPoint remote)
        {
            return await Task.Run(async () =>
            {
                byte[] bytes = new byte[8192];
                EndPoint receivePoint = new IPEndPoint(IPAddress.Any, 0);
                socket.ReceiveFrom(bytes, ref receivePoint);
                socket.SendTo(data, receivePoint);
                return bytes;
            });
        }
        async ValueTask<byte[]> makeClientSession(Socket socket, byte[] data, IPEndPoint remote)
        {
            return await Task.Run(() =>
            {
                socket.SendTo(data, remote);
                byte[] bytes = new byte[8192];
                EndPoint receivePoint = new IPEndPoint(IPAddress.Any, 0);
                socket.ReceiveFrom(bytes, ref receivePoint);
                return bytes;
            });
        }
        public async ValueTask<byte[]> MakeSession(Socket socket, byte[] data, IPEndPoint remote, bool isReceiver)
        {
            if (isReceiver)
            {
                return await makeServerSession(socket, data, remote);
            }
            else
            {
                return await makeClientSession(socket, data, remote);
            }
        }
    }
}
