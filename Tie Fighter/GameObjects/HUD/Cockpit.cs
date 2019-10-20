namespace Tie_Fighter.GameObjects.HUD
{
    /// <summary>
    /// Cockpit class is a GameObject that shows a simple screen overlay (HUD) on the screen.
    /// </summary>
    public class Cockpit : GameObject
    {
        /// <summary>
        /// Used to draw a cockpit (which is a static HUD element). Which means it should overlap the crosshair and TieFighters.
        /// </summary>
        /// <param name="mediaPlayer">Used to play sounds, not used in the case of a HUD.</param>
        /// <param name="x">Used to determine the x drawing position.</param>
        /// <param name="y">Used to determine the y drawing position.</param>
        /// <param name="width">Used to determine the width of the HUD (should be fullscreen).</param>
        /// <param name="height">Used to determine the height of the HUD (should be fullscreen)</param>
        public Cockpit(Others.MediaPlayerHandler mediaPlayer, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap = Properties.Resources.Cockpit;
        }
    }
}
