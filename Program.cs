using ServiceStack.Text;
using SockNet;
using SockNet.ServerSocket;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ShifuServ
{
    class ac
    {
        public static void Socketlst()
        {
            var socketServer = new SocketServer();
            socketServer.InitializeSocketServer("127.0.0.1", 9999);
            socketServer.SetReaderBufferBytes(1024);
            socketServer.StartListening();

            bool openServer = true;
            while (openServer)
            {
                if (socketServer.IsNewData())
                {
                    
                    var data = socketServer.GetData();
                    // Do whatever you want with data
                    //var lobby2 = JsonSerializer.DeserializeFromString<LobbyResponse>(socketServer.GetData());
                    Task.Run(() => DoSomething(data, socketServer));
                }
            }

            //.... 
            socketServer.CloseServer();



        }
        private static void DoSomething(KeyValuePair<TcpClient, byte[]> data, SocketServer server)
        {
            Console.WriteLine(((IPEndPoint)data.Key.Client.RemoteEndPoint).Address.ToString() + ": " + Encoding.UTF8.GetString(data.Value));
            server.ResponseToClient(data.Key, "received");
        }
    }
}