using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Server
{
    [Serializable]
    public struct HighScore
    {
        string name { get; }
        int score { get; }

        public HighScore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
        public int CompareTo(HighScore h)
        {
            return this.score - h.score;
        }
    }

    
}
