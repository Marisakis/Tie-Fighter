using System.Windows.Media;

namespace Tie_Fighter.GameObjects.Crosshairs
{
    public class Crosshair<T> : GameObject<T>
    {
        private Color _crosshairColor;
        public Crosshair(Color crosshairColor)
        {
            this._crosshairColor = crosshairColor;
        }

    }
}
