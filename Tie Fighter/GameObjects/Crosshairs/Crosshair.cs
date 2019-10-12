using System;
using System.Drawing;
using System.Windows.Media;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Crosshairs
{
    public class Crosshair : GameObject
    {
        private System.Drawing.Color _crosshairColor;
        private System.Drawing.Pen pen;
        private RectangleF rectangleF;

        public Crosshair(Others.MediaPlayerHandler mediaPlayer, System.Drawing.Color crosshairColor, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap = Properties.Resources.Cockpit;
        }
    }
}
