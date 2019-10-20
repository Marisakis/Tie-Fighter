namespace Tie_Fighter.GameObjects.HUD
{
    /// <summary>
    /// Wallpaper class can be used to display a background image (for example).
    /// </summary>
    public class Wallpaper : GameObject
    {
        /// <summary>
        /// Used to draw a specific image / wallpaper.
        /// </summary>
        /// <param name="mediaPlayer">Can be used to play sounds, allowed to be null.</param>
        /// <param name="x">x position of the wallpaper.</param>
        /// <param name="y">y position of the wallpaper.</param>
        /// <param name="width">Width of the wallpaper.</param>
        /// <param name="height">Height of the wallpaper.</param>
        public Wallpaper(Others.MediaPlayerHandler mediaPlayer, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap = Properties.Resources.ShooterBG2;
        }
    }
}
