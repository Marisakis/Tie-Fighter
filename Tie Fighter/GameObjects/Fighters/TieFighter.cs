using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.GameObjects.Fighters
{
    public class TieFighter<T> : Fighter<T>
    {
        public TieFighter(T x, T y, T height, T width, T ttp)
        {
            //this.bitmap = new Bitmap(Properties.Resources.smallfighter1);
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            this.TTP = ttp;
            //this.Rescale(width, height);
            this.bitmap = new Bitmap(Properties.Resources.smallfighter1, new Size(Convert.ToInt32(width), Convert.ToInt32(height)));


        }
    }
}
