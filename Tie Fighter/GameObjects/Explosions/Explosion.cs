﻿using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Explosions
{
    public class Explosion<T> : GameObject<T>
    {
        public Explosion(Others.MediaPlayer mediaPlayer) : base(mediaPlayer)
        {
        }

        public T TTL { get; set; } // In milliseconds

        public virtual void PlayFlySound(string URL)
        {
            mediaPlayer.PlayFile(URL);
        }
    }
}
