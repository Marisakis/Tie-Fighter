using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.HUD
{
    /// <summary>
    /// Cockpit class is a GameObject that shows a simple screen overlay (HUD) on the screen.
    /// </summary>
    public class Cockpit : GameObject
    {
        public Cockpit(Others.MediaPlayerHandler mediaPlayer, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap = Properties.Resources.Cockpit;
        }
    }
}
