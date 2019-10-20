using System;
using System.Collections.Generic;

namespace Tie_Fighter.Others
{
    /// <summary>
    /// Handles MediaPlayer instances for optimal multiple sound effects. When a tie fighter is being shot you still want to hear environment music, otherwise this would just be stopped and interrupted by the shooting sound.
    /// </summary>
    public class MediaPlayerHandler
    {
        private readonly List<MediaPlayer> _mediaPlayers;
        private int counter = 0;

        /// <summary>
        /// Default constructor in use.
        /// </summary>
        public MediaPlayerHandler()
        {
            _mediaPlayers = new List<MediaPlayer>();
        }

        /// <summary>
        /// Play a file from source [url] with specific TimeToPlay. Grabs an available MediaPlayer, if there are none available, create a new one.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeToPlay"></param>
        /// <returns></returns>
        public MediaPlayer PlayFile(string url, double? timeToPlay)
        {
            MediaPlayer availableMediaPlayer = GetAvailablePlayer();
            if (availableMediaPlayer == null)
            {
                availableMediaPlayer = new MediaPlayer();
                _mediaPlayers.Add(availableMediaPlayer);
                Console.WriteLine(_mediaPlayers.Count);
            }
            availableMediaPlayer.PlayFile(url, timeToPlay);

            if (counter == 10 && _mediaPlayers.Count > 5)
            {
                counter = 0;
                for (int i = _mediaPlayers.Count - 1; i >= 0; i--)
                {
                    if (_mediaPlayers[i].done)
                    {
                        _mediaPlayers.RemoveAt(i);
                    }
                }
            }
            counter++;
            return availableMediaPlayer;
        }

        /// <summary>
        /// Play a video from URL [url].
        /// </summary>
        /// <param name="url"></param>
        public void PlayVideo(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// Get an available MediaPlayer to output a sound from a URL.
        /// </summary>
        /// <returns></returns>
        public MediaPlayer GetAvailablePlayer()
        {
            foreach (MediaPlayer mediaPlayer in _mediaPlayers)
            {
                if (mediaPlayer.done)
                {
                    return mediaPlayer;
                }
            }

            return null;
        }
    }
}
