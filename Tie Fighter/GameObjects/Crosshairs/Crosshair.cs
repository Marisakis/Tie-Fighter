using System;
using System.Drawing;
using System.Windows.Media;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Crosshairs
{
    public class Crosshair : GameObject
    {
        /// <summary>
        /// Crosshair object. Used to show mouse position to a player.
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <param name="crosshairURL"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Crosshair(Others.MediaPlayerHandler mediaPlayer, string crosshairURL, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap =new Bitmap(crosshairURL);
        }
    }
}
