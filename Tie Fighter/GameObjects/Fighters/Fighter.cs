using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects
{
    public abstract class Fighter : GameObject
    {
        public Fighter(Others.MediaPlayerHandler mediaPlayer, int xPercentage, int yPercentage, int widthPercentage, int heightPercentage) : base(mediaPlayer, xPercentage, yPercentage, widthPercentage, heightPercentage)
        {
        }

        public virtual int TTP { get; set; } // Time to pass, in milliseconds.
        public virtual void PlayFlySound(string URL)
        {
            mediaPlayer.PlayFile(URL, (TTP/1000.0));
        }

        public virtual void MakeTieInterceptor()
        {
            bitmap = Properties.Resources.TieInterceptor;
        }

        public virtual void MakeTieBomber()
        {
            bitmap = Properties.Resources.TieBomber;
        }
    }
}
