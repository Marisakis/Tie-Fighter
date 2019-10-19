using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tie_Server
{
    public class Program : IDataReceiver
    {
       public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormServer());
        }

        TcpListener listener;
        private List<Client> clients = new List<Client>();
        private Dictionary<String, Client> namedClients = new Dictionary<string, Client>();
        private int clientCounter = 0;
        private Object _lockObj = new object();
        private List<Game> games = new List<Game>();
        private string port { get; set; }
 
        public Program(string port)
        {
            this.port = port;
            Console.WriteLine($"Starting server on port: {port}");
            StartAcceptingClientConnections();
            games.Add(new Game());
            while (true)
            {
                bool lockWasTaken = false;

                try
                {
                    System.Threading.Monitor.Enter(_lockObj, ref lockWasTaken);
                    foreach(Game game in games)
                        if(game.gameStatus == GameStatus.Running)
                            game.Tick();
                    System.Threading.Thread.Sleep(GameManager.timerPeriod / 5);
                }
                finally
                {
                    if (lockWasTaken)
                        System.Threading.Monitor.Exit(_lockObj);
                }
                System.Threading.Thread.Sleep(GameManager.timerPeriod/2);

            }
        }

        private void StartAcceptingClientConnections()
        {
            listener = new TcpListener(IPAddress.Any, Int32.Parse(this.port));
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

        public static void handleHighscoreRequest ( Client sender)
        {
            List<HighScore> highscores = Game.GetHighScoresFromFile();
            dynamic reply = new JObject();
            reply.type = "highscores";
            JArray array = new JArray();
            foreach (HighScore h in highscores)
                array.Add(JsonConvert.SerializeObject(h));
            reply.data = array;
            sender.Write(reply);
        }

        public void handlePacket(dynamic data, Client sender)
        {
            //Console.WriteLine("received a message in program");
            Console.WriteLine(data);
            //Console.WriteLine("Data type: " + data.type);
            switch ((string)data.type)
            {
                case "login":
                    namedClients.Add((string)data.name, sender);
                    Player newPlayer = new Player((string)data.name);
                    newPlayer.id = clientCounter++;
                    newPlayer.client = sender;
                    Game currentLobby = games.Last<Game>();
                    if (currentLobby.gameStatus != GameStatus.Lobby)
                        games.Add(new Game());
                    games.Last<Game>().AddPlayer(newPlayer);
                    break;
                case "highscorerequest":
                    handleHighscoreRequest(sender);
                    break;
                case "crosshair":
                    //this.gameManager.UpdatePlayerCrosshair(data.data.clientID, data.data.crosshair);
                    break;
                default:
                    Console.WriteLine("Data type not recognized");
                    break;

            }
        }
    }
}
