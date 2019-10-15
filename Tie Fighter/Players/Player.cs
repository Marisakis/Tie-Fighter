using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tie_Fighter.GameObjects.Crosshairs;

namespace Tie_Fighter.Players
{
    public class Player
    {
        public int id;

        public string name { get; set; }
        public int score { get; set; }
        public Crosshair crosshair { get; set; }

        internal void UpdateScore(int score)
        {
            this.score = score;
        }
    }
}
