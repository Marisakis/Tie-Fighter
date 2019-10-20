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
    /// Class that contains a game. It manages the gathering of players, the 
    /// </summary>

    public enum GameStatus { Lobby, Running, Finished }
    public class Game : IDataReceiver
    {
        private Program main;
        public GameManager gameManager;
        public GameStatus gameStatus { get; set; } = GameStatus.Lobby;

        public Game(Program main)
        {
            this.main = main;
            this.gameManager = new GameManager();
        }

        public void AddPlayer(Player newPlayer)
        {
            newPlayer.client.SetDataReceiver(this);
            gameManager.players.Add(newPlayer);
            dynamic data = new JObject();
            data.type = "chatmessage";
            data.data = "Player " + newPlayer.name + " has joined the lobby";
            foreach (Player player in gameManager.players)
                player.client.Write(data);
            //this.Start();
        }

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

        public void Tick()
        {
            foreach (Player player in gameManager.players)
            {
                Client client = player.client;
                client.Write(gameManager.GetGameData());
            }
        }
        public void Start()
        {
            gameStatus = GameStatus.Running;
            var timerDelegate = new System.Timers.Timer(60000);
            timerDelegate.Elapsed += OnTimedEvent;
            timerDelegate.AutoReset = false;
            timerDelegate.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Finish();
        }

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


        public HighScore GetHighestScore()
        {
            List<HighScore> scores = new List<HighScore>();
            foreach (Player player in gameManager.players)
                scores.Add(new HighScore(player.name, player.score));
            scores.Sort();
            return (scores[0]);
        }

        public static List<HighScore> GetHighScoresFromFile()
        {
            string path = Directory.GetCurrentDirectory();
            path += "highscores.txt";
            List<HighScore> highscores = new List<HighScore>();
            if (!File.Exists(path))
            {
                //highscores.Add(new HighScore("testscore", 100));
                return highscores;
            }
            else
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    highscores = (List<HighScore>)binaryFormatter.Deserialize(fileStream);
                }
                //highscores.Add(new HighScore("testscore", 100));
                return highscores;
            }
        }

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
