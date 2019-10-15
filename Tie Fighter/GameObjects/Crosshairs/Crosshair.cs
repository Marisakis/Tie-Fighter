using System;
using System.Drawing;
using System.Windows.Media;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Crosshairs
{
    public class Crosshair : GameObject
    {
        public Crosshair(Others.MediaPlayerHandler mediaPlayer, string crosshairURL, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap =new Bitmap(crosshairURL);
        }
    }
}
