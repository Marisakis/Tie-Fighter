using Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Tie_Server
{    
    /// <summary>
    /// Defines the current GameStatus, can be either in Lobby, Running or Finished.
    /// </summary>
    public enum GameStatus { Lobby, Running, Finished }

    /// <summary>
    /// Class that contains a game. It manages the gathering of players.
    /// </summary>

    public class Game : IDataReceiver
    {
        private Program main;
        public GameManager gameManager;
        public GameStatus gameStatus { get; set; } = GameStatus.Lobby;

        /// <summary>
        /// Default constructor sets main and creates a GameManager.
        /// </summary>
        /// <param name="main"></param>
        public Game(Program main)
        {
            this.main = main;
            this.gameManager = new GameManager();
        }

        /// <summary>
        /// Add a player to the game.
        /// </summary>
        /// <param name="newPlayer"></param>
        public void AddPlayer(Player newPlayer)
        {
            newPlayer.client.SetDataReceiver(this);
            gameManager.players.Add(newPlayer);
            dynamic data = new JObject();
            data.type = "chatmessage";
            data.data = "Player " + newPlayer.name + " has joined the lobby";
            foreach (Player player in gameManager.players)
                player.client.Write(data);
        }
        /// <summary>
        /// Handle a received packet, for example force start the game.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sender"></param>
        public void handlePacket(dynamic data, Client sender)
        {
            switch ((string)data.type)
            {
                case "crosshair":
                    gameManager.UpdatePlayerCrosshair(sender, data.data);
                    break;
                case "highscorerequest":
                    Program.handleHighscoreRequest(sender);
                    break;
                case "startgame":
                    this.Start();
                    break;
                case "chatmessage":
                    foreach (Player player in gameManager.players)
                        player.client.Write(data);
                    break;
                default:
                    Console.WriteLine("Data type not recognised");
                    break;
            }
        }

        /// <summary>
        /// Upon each timer Tick index the GameData.
        /// </summary>
        public void Tick()
        {
            foreach (Player player in gameManager.players)
            {
                Client client = player.client;
                client.Write(gameManager.GetGameData());
            }
        }

        /// <summary>
        /// Each game runs for 1 minute, then goes back to the lobby.
        /// </summary>
        public void Start()
        {
            gameStatus = GameStatus.Running;
            var timerDelegate = new System.Timers.Timer(60000);
            timerDelegate.Elapsed += OnTimedEvent;
            timerDelegate.AutoReset = false;
            timerDelegate.Enabled = true;
        }

        /// <summary>
        /// Timed event calls finish method so the game stops and players return to the lobby.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Finish();
        }

        /// <summary>
        /// Finish the game and save the highscores.
        /// </summary>
        private void Finish()
        {
            Console.WriteLine("Ending game");
            this.gameStatus = GameStatus.Finished;

            List<HighScore> scores = GetHighScoresFromFile();
            scores.Add(GetHighestScore());
            scores.Sort();
            if (scores.Count > 10)
                scores.RemoveRange(10, scores.Count - 10); // trim so only 10 remain
            writeHighscoresToFile(scores);
            dynamic data = new JObject();
            data.type = "gameended";
            foreach (Player player in gameManager.players)
            {
                main.AssignPlayerToGame(player.client, player.name);
                player.client.Write(data);
            }
            this.gameManager = new GameManager();
            this.gameStatus = GameStatus.Lobby;

        }


        /// <summary>
        /// Receive the highest score.
        /// </summary>
        /// <returns></returns>
        public HighScore GetHighestScore()
        {
            List<HighScore> scores = new List<HighScore>();
            foreach (Player player in gameManager.players)
                scores.Add(new HighScore(player.name, player.score));
            scores.Sort();
            return (scores[0]);
        }

        /// <summary>
        /// Receive highscores from a file.
        /// </summary>
        /// <returns></returns>
        public static List<HighScore> GetHighScoresFromFile()
        {
            string path = Directory.GetCurrentDirectory();
            path += "highscores.txt";
            List<HighScore> highscores = new List<HighScore>();
            if (!File.Exists(path))
            {
                return highscores;
            }
            else
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    highscores = (List<HighScore>)binaryFormatter.Deserialize(fileStream);
                }
                return highscores;
            }
        }

        /// <summary>
        /// Write highscores to a file.
        /// </summary>
        /// <param name="highscores"></param>
        public static void writeHighscoresToFile(List<HighScore> highscores)
        {
            string path = Directory.GetCurrentDirectory();
            path += "highscores.txt";
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, highscores);
            }
        }


    }
}
