using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects
{
    public abstract class Fighter : GameObject
    {
        public Fighter(Others.MediaPlayer mediaPlayer, int x, int y, int width, int height) : base(mediaPlayer, x, y, width, height)
        {
        }

        public virtual int TTP { get; set; } // Time to pass, in milliseconds.
        public virtual void PlayFlySound(string URL)
        {
            mediaPlayer.PlayFile(URL);
        }
    }
}
