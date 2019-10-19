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

        public Player() { }
        public Player(string name, int score) { this.name = name; this.score = score; }
        public Player(string name, int score, Crosshair crosshair) { this.name = name; this.score = score; this.crosshair = crosshair; }

        public int x =-10;
        public int y =-10;
        public int w =0;
        public int h =0;

        internal void UpdateScore(int score)
        {
            this.score = score;
        }
    }
}
