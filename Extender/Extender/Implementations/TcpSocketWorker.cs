using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace Extender.Implementations
{
    public readonly struct TcpSocketWorker : ISocketWorker
    {
        public void Bind(Socket socket, IPAddress address, short port)
        {
            socket.Bind(new IPEndPoint(address, port));
        }
        async ValueTask<byte[]> makeServerSession(Socket socket, byte[] data, IPEndPoint remote)
        {
            return await Task.Run(async () =>
            {
                Socket accepter = await socket.AcceptAsync();
                List<byte> bytes = new List<byte>(8192);
                do
                {
                    byte[] temp = new byte[8192];
                    int counted = accepter.Receive(temp, SocketFlags.None);
                    Array.Resize(ref temp, counted);
                    bytes.AddRange(temp);
                } while (accepter.Available > 0);
                accepter.Send(data);
                accepter.Shutdown(SocketShutdown.Both);
                accepter.Close();
                return bytes.ToArray();
            });
        }
        async ValueTask<byte[]> makeClientSession(Socket socket, byte[] data, IPEndPoint remote)
        {
            return await Task.Run(() =>
            {
                socket.Connect(remote);
                socket.Send(data);
                List<byte> bytes = new List<byte>(8192);
                do
                {
                    byte[] temp = new byte[8192];
                    int counted = socket.Receive(temp, SocketFlags.None);
                    Array.Resize(ref temp, counted);
                    bytes.AddRange(temp);
                } while (socket.Available > 0);
                return bytes.ToArray();
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
