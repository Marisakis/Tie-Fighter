using Networking;
using Tie_Server.GameObjects;

namespace Tie_Server
{
    /// <summary>
    /// The Player class keeps track of a player ID, name, score and crosshair position.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// In the default constructor the name is set, also a dummy ID is initialized and score is set to 0. A new crosshair is created out of the screen.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            this.name = name;
            id = 500;
            score = 0;
            crosshair = new Crosshair(0, -10, -10, 10, 10);
        }

        /// <summary>
        /// Client can be retrieved to know "who" the player is.
        /// </summary>
        public Client client { get; set; }
        /// <summary>
        /// Can be used to identify the player by their ID.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// A name is used to show in the highscore table.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// A score is used to keep track of the leaderboard.
        /// </summary>
        public int score { get; set; }

        public Crosshair crosshair { get; set; }
        /// <summary>
        /// Update the crosshair x and y position, and whether it is firing (does not mean it is a hit! It's just a boolean!).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isFiring"></param>
        internal void UpdateCrosshair(int x, int y, bool isFiring)
        {
            crosshair.x = x;
            crosshair.y = y;
            crosshair.isFiring = isFiring;
        }
    }
}
