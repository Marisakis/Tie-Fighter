using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects
{
    public abstract class Fighter<T> : GameObject<T>
    {
        public Fighter(Others.MediaPlayer mediaPlayer, T xPercentage, T yPercentage, T widthPercentage, T heightPercentage) : base(mediaPlayer, xPercentage, yPercentage, widthPercentage, heightPercentage)
        {
        }

        public virtual T TTP { get; set; } // Time to pass, in milliseconds.
        public virtual void PlayFlySound(string URL)
        {
            mediaPlayer.PlayFile(URL);
        }
    }
}
