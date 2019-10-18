using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        private void StartNewGame()
        {

        }
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
                    {
                        client.Write(gameManager.GetGameData());
                    }
                }
                finally
                {
                    if (lockWasTaken) System.Threading.Monitor.Exit(_lockObj);
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
                //Console.WriteLine("New client connected, Clients: " + clients.Count);
                listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), this);
            } finally
            {
                if (lockWasTaken) System.Threading.Monitor.Exit(_lockObj);
            }
        }

        public void handlePacket(dynamic data, Client sender)
        {
            //Console.WriteLine("received a message in program");
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
                    this.gameManager.UpdatePlayerCrosshair(data.data.clientID, data.data.crosshair);
                    break;
                case "highscorerequest":
                    List<HighScore> highscores = Game.GetHighScoresFromFile();
                    dynamic reply = new JObject();
                    reply.type = "highscores";
                    JArray array = new JArray();
                    foreach(HighScore h in highscores)
                        array.Add(JsonConvert.SerializeObject(h));
                    reply.data = array;
                    sender.Write(reply);
                    break;
                default:
                    Console.WriteLine("Data type not recognised");
                    break;

            }
        }


    }
}
