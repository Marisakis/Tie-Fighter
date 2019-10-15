using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Server.GameObjects
{
    class Target: GameObject
    {
        public Target(int TTP, int id, int x, int y, int width, int height) : base(id, x, y, width, height)
        {
            this.TTP = TTP;
        }

        public int TTP { get; set; }

        
    }
}
