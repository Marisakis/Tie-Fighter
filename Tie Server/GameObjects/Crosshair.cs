using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Server.GameObjects
{
    /// <summary>
    /// Crosshair object.
    /// </summary>
    public class Crosshair : GameObject
    {
        public Crosshair(int id, double x, double y, int width, int height) : base(id, x, y, width, height)
        {
            this.isFiring = false;
        }

        public Boolean isFiring { get; set; }
    }
}
