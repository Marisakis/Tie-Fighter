using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.HUD
{
    /// <summary>
    /// Wallpaper class can be used to display a background image (for example).
    /// </summary>
    public class Wallpaper : GameObject
    {
        public Wallpaper(Others.MediaPlayerHandler mediaPlayer, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap = Properties.Resources.ShooterBG2;
        }
    }
}
