using Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tie_Server.GameObjects;

namespace Tie_Server
{
    public class Player
    {

        public Player(string name)
        {
            this.name = name;
            this.id = 500;
            this.score = 0;
            this.crosshair = new Crosshair(0,-10,-10,10,10);
            
        }

        public Client client { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int score { get; set; }

        Crosshair crosshair { get; set; }

        internal void UpdateCrosshair(int x, int y, bool isFiring)
        {
            this.crosshair.x = (double)x;
            this.crosshair.y = (double)y;
            this.crosshair.isFiring = isFiring;
        }
    }
}
