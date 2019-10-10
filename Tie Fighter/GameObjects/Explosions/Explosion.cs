using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Explosions
{
    public class Explosion : GameObject
    {
        public Explosion(Others.MediaPlayer mediaPlayer, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
        }

        public int TTL { get; set; } // In milliseconds

        public virtual void PlayFlySound(string URL)
        {
            mediaPlayer.PlayFile(URL);
        }
    }
}
