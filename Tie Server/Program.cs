using Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Tie_Server
{
    /// <summary>
    /// The Program class initializes the server.
    /// </summary>
    public class Program : IDataReceiver
    {
        /// <summary>
        /// The Main method launches the server GUI window.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormServer());
        }

        private TcpListener listener;
        private readonly List<Client> clients = new List<Client>();
        private int clientCounter = 0;
        private readonly object _lockObj = new object();
        private readonly List<Game> games = new List<Game>();
        private string port { get; set; }

        /// <summary>
        /// Launch the server on port [port]. Thread.Sleep is being used to prevent buffer overflowing and processor maxing.
        /// </summary>
        /// <param name="port"></param>
        public Program(string port)
        {
            this.port = port;
            Console.WriteLine($"Starting server on port: {port}");
            StartAcceptingClientConnections();
            games.Add(new Game(this));
            while (true)
            {
                bool lockWasTaken = false;
                try
                {
                    System.Threading.Monitor.Enter(_lockObj, ref lockWasTaken);
                    foreach (Game game in games)
                    {
                        if (game.gameStatus == GameStatus.Running)
                        {
                            game.Tick();
                        }
                    }
                }
                finally
                {
                    if (lockWasTaken)
                    {
                        System.Threading.Monitor.Exit(_lockObj);
                    }
                }
                System.Threading.Thread.Sleep(GameManager.timerPeriod / 5);
            }
        }

        /// <summary>
        /// Accept client connections from any IP address from any port.
        /// </summary>
        private void StartAcceptingClientConnections()
        {
            listener = new TcpListener(IPAddress.Any, int.Parse(port));
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), this);
        }

        /// <summary>
        /// When a client has connected, the client is added to the list. For obligated Thread safe conditions a lock object and boolean is used.
        /// </summary>
        /// <param name="ar"></param>
        private void OnConnect(IAsyncResult ar)
        {
            bool lockWasTaken = false;
            try
            {
                System.Threading.Monitor.Enter(_lockObj, ref lockWasTaken);
                TcpClient newTcpClient = listener.EndAcceptTcpClient(ar);
                clients.Add(new Client(newTcpClient, this));
                listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), this);
            }
            finally
            {
                if (lockWasTaken)
                {
                    System.Threading.Monitor.Exit(_lockObj);
                }
            }
        }
        /// <summary>
        /// Reply to a highscore request with the specific information.
        /// </summary>
        /// <param name="sender"></param>
        public static void handleHighscoreRequest(Client sender)
        {
            List<HighScore> highscores = Game.GetHighScoresFromFile();
            dynamic reply = new JObject();
            reply.type = "highscores";
            JArray array = new JArray();
            foreach (HighScore h in highscores)
            {
                array.Add(JsonConvert.SerializeObject(h));
            }

            reply.data = array;
            sender.Write(reply);
        }

        /// <summary>
        /// Assign a player with name [name] to a specific game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="name"></param>
        public void AssignPlayerToGame(Client sender, string name)
        {
            Player newPlayer = new Player(name)
            {
                id = clientCounter++,
                client = sender
            };
            GetActiveLobby().AddPlayer(newPlayer);
        }

        /// <summary>
        /// Receive an active lobby, if none exist, create one.
        /// </summary>
        /// <returns></returns>
        private Game GetActiveLobby()
        {
            foreach (Game game in games)
            {
                if (game.gameStatus == GameStatus.Lobby)
                {
                    return game;
                }
            }
            Game newGame = new Game(this);
            games.Add(newGame);
            Console.WriteLine("Games: " + games.Count);
            return newGame;
        }

        /// <summary>
        /// Handle a client packet, in case of login, a player is assigned to a game. In case of highscorerequest a HandleHighscoreRequest(sender) is called, etc.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sender"></param>
        public void handlePacket(dynamic data, Client sender)
        {
            switch ((string)data.type)
            {
                case "login":
                    AssignPlayerToGame(sender, (string)data.name);
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
