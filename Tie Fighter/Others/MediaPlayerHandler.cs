using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Others
{
    public class MediaPlayerHandler
    {
        private List<MediaPlayer> _mediaPlayers;
        private int counter = 0;

        public MediaPlayerHandler()
        {
            this._mediaPlayers = new List<MediaPlayer>();
        }

        public void PlayFile(string url, double? timeToPlay)
        {
            MediaPlayer availableMediaPlayer = GetAvailablePlayer();
            if (availableMediaPlayer == null)
            {
                availableMediaPlayer = new MediaPlayer();
                _mediaPlayers.Add(availableMediaPlayer);
                Console.WriteLine(_mediaPlayers.Count);
            }
            availableMediaPlayer.PlayFile(url, timeToPlay);

            if (counter==10)
            {
                counter = 0;
                for (int i = _mediaPlayers.Count-1; i >=0; i--)
                {
                    if (_mediaPlayers[i].done)
                    {
                        _mediaPlayers.RemoveAt(i);
                    }
                }
            }
            counter++;
        }

        public void PlayVideo(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

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
