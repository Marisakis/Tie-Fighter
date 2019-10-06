using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.GameObjects
{
    public abstract class GameObject<T>
    {
        public virtual T x { get; set; } // X-position.
        public virtual T y { get; set; } // Y-position.
        public virtual Tuple<T, T> GetPosition()
        {
            return new Tuple<T, T>(x, y);
        }
        public virtual void Draw(Bitmap bitmap)
        {

        }
    }
}
