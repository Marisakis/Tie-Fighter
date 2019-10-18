using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Server
{
    /// <summary>
    /// Class that contains a game and 
    /// </summary>
    public class Game
    {
        private GameManager gameManager;

        public void Start()
        {

        }


        private void Finish()
        {
        }


        public HighScore GetHighestScore(List<Player> players)
        {
            //PLACEHOLDER CODE, REPLACE LATER
            Player highestScore = null;
            return new HighScore();
        }

        public static List<HighScore> getHighScoresFromFile()
        {
            string path = Directory.GetCurrentDirectory();
            path += "highscores.txt";
            List<HighScore> highscores = new List<HighScore>();
            if (!File.Exists(path))
                return highscores;
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
