using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Explosions
{
    /// <summary>
    /// Explosion is shown after a TieFighter has been destroyed.
    /// </summary>
    public class Explosion : GameObject
    {
        /// <summary>
        /// The Explosion class is used to draw an explosion on the screen after shooting a TieFighter.
        /// </summary>
        /// <param name="mediaPlayer">Used to play [boom] sound effect.</param>
        /// <param name="x">Defines the x position of the explosion.</param>
        /// <param name="y">Defines the y position of the explosion.</param>
        /// <param name="width">Defines the explosion width.</param>
        /// <param name="height">Defines the explosion height.</param>
        public Explosion(Others.MediaPlayerHandler mediaPlayer, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap = Properties.Resources.Explode;
        }

        /// <summary>
        /// Defines the TimeToLive of the explosion.
        /// </summary>
        public int TTL { get; set; } 

        /// <summary>
        /// Play the explosion sound from source [url].
        /// </summary>
        /// <param name="URL"></param>
        public virtual void PlayExplosionSound(string URL)
        {
            mediaPlayer.PlayFile(URL, null);
        }
    }
}
