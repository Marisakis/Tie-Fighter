using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Others
{
    /// <summary>
    /// Handles MediaPlayer instances for optimal multiple sound effects. When a tie fighter is being shot you still want to hear environment music, otherwise this would just be stopped and interrupted by the shooting sound.
    /// </summary>
    public class MediaPlayerHandler
    {
        private List<MediaPlayer> _mediaPlayers;
        private int counter = 0;

        public MediaPlayerHandler()
        {
            this._mediaPlayers = new List<MediaPlayer>();
        }

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
           
            if (counter==10 && _mediaPlayers.Count>5)
            {
                counter = 0;
                for (int i = _mediaPlayers.Count-1; i >=0; i--)
                    if (_mediaPlayers[i].done)
                        _mediaPlayers.RemoveAt(i);
            }
            counter++;
            return availableMediaPlayer;
        }

        public void PlayVideo(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        public MediaPlayer GetAvailablePlayer()
        {
            foreach (MediaPlayer mediaPlayer in _mediaPlayers)
                if (mediaPlayer.done)
                    return mediaPlayer;
            return null;
        }
    }
}
