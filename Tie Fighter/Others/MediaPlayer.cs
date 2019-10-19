﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Others
{
    public class MediaPlayer
    {
        WMPLib.WindowsMediaPlayerClass player;
        public bool done = false;
        public MediaPlayer()
        {
            player = new WMPLib.WindowsMediaPlayerClass();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeToPlay">Use 2.0 for double speed, 0.5 for half speed, null or -1 to leave unchanged</param>
        public void PlayFile(string url, double? timeToPlay)
        {
            try
            {
                done = false;
                player.PlayStateChange += Player_PlayStateChange;
                player.URL = url;
                if (timeToPlay != null || timeToPlay == -1)
                {
                    double duration = player.newMedia(url).duration;
                    player.settings.rate = (duration / (double)timeToPlay);
                }
                player.controls.play();
            }
            catch (Exception e) { }
        }

        public void Stop()
        {
            player.controls.stop();
            player.close();
            done = true;
        }

        public void EndPlay()
        {
            player.stop();
        }

        public void Player_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped && done==false)
            {
                player.controls.stop();
                player.close();
                done = true;
                //Actions on stop
            }
        }
    }
}
