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
    public class Program : IDataReceiver
    {
        static void Main(string[] args)
        {
            new Program();

        }

        GameManager gameManager;
        TcpListener listener;
        private List<Client> clients = new List<Client>();
        private Dictionary<String, Client> namedClients = new Dictionary<string, Client>();
        private int clientCounter = 0;
        private Object _lockObj = new object();

        private Program()
        {
            Console.WriteLine("Starting server");
            this.gameManager = new GameManager();

            StartAcceptingClientConnections();
            while (true)
            {
                bool lockWasTaken = false;
                try
                {
                    System.Threading.Monitor.Enter(_lockObj, ref lockWasTaken);
                    foreach (Client client in clients)
                        client.Write(gameManager.GetGameData());
                    System.Threading.Thread.Sleep(gameManager.timerPeriod / 5);
                }
                finally
                {
                    if (lockWasTaken)
                        System.Threading.Monitor.Exit(_lockObj);
                }
            }
        }

        private void StartAcceptingClientConnections()
        {
            listener = new TcpListener(IPAddress.Any, 1717);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), this);
        }

        private void OnConnect(IAsyncResult ar)
        {
            bool lockWasTaken = false;
            try
            {
                System.Threading.Monitor.Enter(_lockObj, ref lockWasTaken);
                var newTcpClient = listener.EndAcceptTcpClient(ar);
                clients.Add(new Client(newTcpClient, this));
                listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), this);
            }
            finally
            {
                if (lockWasTaken) System.Threading.Monitor.Exit(_lockObj);
            }
        }

        public void handlePacket(dynamic data, Client sender)
        {
            //Console.WriteLine("received a message in program");
            Console.WriteLine(data);
            Console.WriteLine("Data type: " + data.type);
            switch ((string)data.type)
            {
                case "login":
                    namedClients.Add((string)data.name, sender);
                    Player newPlayer = new Player((string)data.name);
                    newPlayer.id = clientCounter++;
                    this.gameManager.players.Add(newPlayer);
                    break;
                case "crosshair":
                    //this.gameManager.UpdatePlayerCrosshair(data.data.clientID, data.data.crosshair);
                    break;
                default:
                    Console.WriteLine("Data type not recognised");
                    break;

            }
        }


    }
}
