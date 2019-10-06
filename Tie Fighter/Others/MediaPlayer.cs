using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Others
{
    public class MediaPlayer
    {
        WMPLib.WindowsMediaPlayer player;

        public MediaPlayer()
        {
            player = new WMPLib.WindowsMediaPlayer();
        }

        public void PlayFile(String url)
        {
            player.PlayStateChange += Player_PlayStateChange;
            player.URL = url;
            player.controls.play();
        }

        public void Player_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
                //Actions on stop
            }
        }
    }
}
