using System.Windows.Media;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Crosshairs
{
    public class Crosshair<T> : GameObject<T>
    {
        private Color _crosshairColor;

        public Crosshair(Others.MediaPlayer mediaPlayer, Color crosshairColor, T xPercentage, T yPercentage, T widthPercentage, T heightPercentage) : base(mediaPlayer, xPercentage, yPercentage, widthPercentage, heightPercentage)
        {
            this._crosshairColor = crosshairColor;
        }
    }
}
