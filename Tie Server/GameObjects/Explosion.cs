using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Server.GameObjects
{
    class Explosion: GameObject
    {
        public Explosion(int TTL, int id, int x, int y, int width, int height) : base(id, x, y, width, height)
        {
            this.TTL = TTL;
        }

        public int TTL { get; set; }
    }
}
