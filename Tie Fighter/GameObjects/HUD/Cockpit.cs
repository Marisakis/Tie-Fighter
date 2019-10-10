using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.HUD
{
    public class Cockpit<T> : GameObject<T>
    {
        public Cockpit(Others.MediaPlayer mediaPlayer, T xPercentage, T yPercentage, T widthPercentage, T heightPercentage) : base(mediaPlayer, xPercentage, yPercentage, widthPercentage, heightPercentage)
        {
            bitmap = Properties.Resources.Cockpit;
        }
    }
}
