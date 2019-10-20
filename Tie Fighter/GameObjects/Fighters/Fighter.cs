namespace Tie_Fighter.GameObjects
{
    /// <summary>
    /// Fighter class can play an engine sound (Tie Fighter roar).
    /// </summary>
    public abstract class Fighter : GameObject
    {
        /// <summary>
        /// Is used to draw a TieFighter / TieInterceptor or TieBomber on the screen.
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <param name="xPercentage"></param>
        /// <param name="yPercentage"></param>
        /// <param name="widthPercentage"></param>
        /// <param name="heightPercentage"></param>
        public Fighter(Others.MediaPlayerHandler mediaPlayer, int xPercentage, int yPercentage, int widthPercentage, int heightPercentage) : base(mediaPlayer, xPercentage, yPercentage, widthPercentage, heightPercentage)
        {
        }

        /// <summary>
        /// Defines the TimeToPass (TTP), should be in seconds. Sound effect is being based on this value, so make sure you fill this value correctly.
        /// </summary>
        public virtual int TTP { get; set; }
        /// <summary>
        /// Play a specific fly sound from source [URL].
        /// </summary>
        /// <param name="URL"></param>
        public virtual void PlayFlySound(string URL)
        {
            Others.MediaPlayer usedMP = mediaPlayer.PlayFile(URL, (TTP / 1000.0));
            usedMediaPlayer = usedMP;
        }
        /// <summary>
        /// Replace the Bitmap image by a Tie Interceptor (when TTP is lower than x). Tie Interceptors are faster.
        /// </summary>
        public virtual void MakeTieInterceptor()
        {
            bitmap = Properties.Resources.TieInterceptor;
        }
        /// <summary>
        /// Replace the Bitmap image by a Tie Bomber (when TTP is higher than x). Tie Bombers are slow.
        /// </summary>
        public virtual void MakeTieBomber()
        {
            bitmap = Properties.Resources.TieBomber;
        }
    }
}
