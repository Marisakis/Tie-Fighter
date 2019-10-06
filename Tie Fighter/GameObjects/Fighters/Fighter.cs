namespace Tie_Fighter.GameObjects
{
    public abstract class Fighter<T> : GameObject<T>
    {
        public virtual T TTP { get; set; } // Time to pass, in milliseconds.
        public virtual void PlayFlySound(string URL)
        {
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = URL;
            wplayer.controls.play();
        }
    }
}
