using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Server
{
    /// <summary>
    /// The HighScore class keeps track of a player name and score, and can compare it.
    /// </summary>
    [Serializable]
    public struct HighScore: IComparable<HighScore>
    {
        public string name { get; }
        public int score { get; }

        /// <summary>
        /// Default constructor sets the name and score.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public HighScore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        /// <summary>
        /// Compare to another highscore.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public int CompareTo(HighScore h)
        {
            return h.score - this.score;
        }

        /// <summary>
        /// ToString method prints the highscore.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return this.name + ": " + this.score;
        }
    }
}
