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
    public class Program: IDataReceiver
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

        private Program()
        {
            Console.WriteLine("Starting server");
            this.gameManager  = new GameManager();

            /*Console.WriteLine("Json test");
            gameManager.GetGameData();
*/
            StartAcceptingClientConnections();
            while(true)
            {
                Console.ReadKey();
                namedClients["seb"].Write(gameManager.GetGameData());
            }
        }

        private void StartAcceptingClientConnections()
        {
            listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), this);
        }

        private void OnConnect(IAsyncResult ar)
        {
            var newTcpClient = listener.EndAcceptTcpClient(ar);
            clients.Add(new Client(newTcpClient, this));
            Console.WriteLine("New client connected, Clients: " + clients.Count);
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), this);
        }

        public void handlePacket(dynamic data, Client sender)
        {
            //Console.WriteLine("received a message in program");
            Console.WriteLine("Data type: " + data.type);
            switch ((string)data.type)
            {
                case "login":
                    namedClients.Add((string)data.name, sender);
                    //Console.WriteLine("x");
                    //Console.WriteLine("Added client: " + (string)data.name + " to dictionary");
                    Player newPlayer = new Player((string)data.name);
                    newPlayer.id = clientCounter++;
                    this.gameManager.players.Add(newPlayer);
                    break;
                case "crosshair":
                    this.gameManager.UpdatePlayerCrosshair(data.data.clientID, data.data.crosshair);
                    break;
                 default:
                    Console.WriteLine("Data type not recognised");
                    break;
                 
            }
        }

     
    }
}
