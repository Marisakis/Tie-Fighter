using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Server.GameObjects
{
    /// <summary>
    /// General GameObject.
    /// </summary>
    public abstract class GameObject
    {
       
        public int id { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        protected GameObject(int id, double x, double y, int width, int height)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

    }
}
