using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Networking;

namespace Tie_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        TcpListener listener;
        private List<Client> clients = new List<Client>();

        Program()
        {
            Console.WriteLine("Starting server");
            StartAcceptingClientConnections();
        }

        private void StartAcceptingClientConnections()
        {
            listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.ReadKey();
        }

        private void OnConnect(IAsyncResult ar)
        {
            var newTcpClient = listener.EndAcceptTcpClient(ar);
            Console.WriteLine("New client connected, Clients: " + clients.Count);
            clients.Add(new Client(newTcpClient, this));
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }
    }
}
