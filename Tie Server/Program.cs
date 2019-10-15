using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Networking;

namespace Tie_Server
{
    class Program: IDataReceiver
    {
        static void Main(string[] args)
        {
            new Program();
        }

        TcpListener listener;
        private List<Client> clients = new List<Client>();
        private Dictionary<String, Client> namedClients = new Dictionary<string, Client>();

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
            clients.Add(new Client(newTcpClient, this));
            Console.WriteLine("New client connected, Clients: " + clients.Count);
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        public void handlePacket(string[] data, Client sender)
        {
            int i = 0;
            while (data.Length > i)
            {
                switch (data[i])
                {
                    case "<login>":
                        {
                            Debug.WriteLine("Handling player login");
                            Console.WriteLine("Player is called: " + data[i+1]);
                            i += 2;
                            //Todo: actually handle player login
                            break;
                        }
                    case "<crosshair>":
                        {
                            Debug.WriteLine("Handling crosshair position");
                            double x = Convert.ToDouble(data[i + 1]);
                            double y = Convert.ToDouble(data[i + 2]);
                            Boolean firing = Boolean.Parse(data[i + 3]);
                            i += 3;
                            Console.WriteLine("Fired at "+ x + "," + y + " " + firing);
                            //todo actually handle crosshair data
                            break;
                        }
                    case "":
                        {
                            Debug.WriteLine("Empty data string");
                            i++;
                            break;
                        }
                }
            }
        }
    }
}
