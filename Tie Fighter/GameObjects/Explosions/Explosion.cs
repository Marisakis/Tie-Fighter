using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Explosions
{
    public class Explosion : GameObject
    {
        public Explosion(Others.MediaPlayerHandler mediaPlayer, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
            bitmap = Properties.Resources.Explode;
        }

        public int TTL { get; set; } // In milliseconds

        public virtual void PlayExplosionSound(string URL)
        {
            mediaPlayer.PlayFile(URL, null);
        }
    }
}
