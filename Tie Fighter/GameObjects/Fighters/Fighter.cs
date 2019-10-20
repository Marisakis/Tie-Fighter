using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects
{
    /// <summary>
    /// Fighter class can play an engine sound (Tie Fighter roar).
    /// </summary>
    public abstract class Fighter : GameObject
    {
        public Fighter(Others.MediaPlayerHandler mediaPlayer, int xPercentage, int yPercentage, int widthPercentage, int heightPercentage) : base(mediaPlayer, xPercentage, yPercentage, widthPercentage, heightPercentage)
        {
        }

        public virtual int TTP { get; set; } // Time to pass, in milliseconds.
        public virtual void PlayFlySound(string URL)
        {
            Others.MediaPlayer usedMP = mediaPlayer.PlayFile(URL, (TTP/1000.0));
            this.usedMediaPlayer = usedMP;
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
