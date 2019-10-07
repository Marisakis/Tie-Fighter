using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.GameObjects.Fighters
{
    class TieFighter<T> : Fighter<T>
    {
        public TieFighter()
        {
            this.bitmap = new Bitmap(Properties.Resources.smallfighter1);
        }
    }
}
