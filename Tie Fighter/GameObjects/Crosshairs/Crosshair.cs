using System.Drawing;

namespace Tie_Fighter.GameObjects.Crosshairs
{
    public class Crosshair : GameObject
    {
        /// <summary>
        /// Crosshair object. Used to show mouse position to a player.
        /// </summary>
        /// <param name="mediaPlayer">Needed to play sound effects.</param>
        /// <param name="crosshairURL">Needed to display the crosshair.</param>
        /// <param name="x">Needed to draw on this x-position.</param>
        /// <param name="y">Needed to draw on this y-position.</param>
        /// <param name="width">Defines the width of the crosshair.</param>
        /// <param name="height">Defines the height of the crosshair.</param>
        public Crosshair(Others.MediaPlayerHandler mediaPlayer, string crosshairURL, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap = new Bitmap(crosshairURL);
        }
    }
}
