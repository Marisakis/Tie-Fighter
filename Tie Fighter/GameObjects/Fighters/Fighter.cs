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
            mediaPlayer.PlayFile(URL, TTP);
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType().Equals( this.GetType()))
            {
                Fighter f = (Fighter)obj;
                return (f.id == this.id);
            }
            return base.Equals(obj);
        }
    }
}
