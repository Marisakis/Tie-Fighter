using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Others
{
    /// <summary>
    /// Can retrieve directories from the DirectoryManager. Handy to store specific locations to, for example, audio or video files.
    /// </summary>
    public class DirectoryManager
    {
        //Leap Motion SDK.
        public string LeapDir { get; }

        //Audio files.
        public string AudioDir { get; }
        public string VideoDir { get; }
        public string FireSound { get; }
        public string TieFighterFlyBy { get; }

        public string TieExplodeSound { get; }
        public string Good { get; }
        public string Bad { get; }

        //Video files.
        public string IntroVideo { get; }

        //Image files.
        public string ImageDir { get; }
        public string CrosshairDir { get; }

        //Get crosshair url.
        public string Crosshair(byte number)
        {
            return $"{CrosshairDir}/crosshair{number}.png";
        }
        
        /// <summary>
        /// Build directory paths.
        /// </summary>
        public DirectoryManager()
        {
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string tieFighterPath = $"{documentsFolder}/TieFighter";
            if (!Directory.Exists(tieFighterPath))
            {
                Directory.CreateDirectory(tieFighterPath);
                throw new Exception(@"You are missing the ""TieFighter"" folder in your ""My Documents"" folder, please make sure all assets are in the correct place.");
            }
            else
            {
                //Leap
                this.LeapDir = $"{tieFighterPath}/Leap";

                //Audio
                this.AudioDir = $"{tieFighterPath}/Audio";
                this.FireSound = $"{AudioDir}/Player/tie_fire.mp3";
                this.TieFighterFlyBy = $"{AudioDir}/TieFighter/flyby2.mp3";
                this.TieExplodeSound = $"{AudioDir}/TieFighter/explode.wav";
                this.Good = $"{AudioDir}/GameMusic/goodsmall.mp3";
                this.Bad = $"{AudioDir}/GameMusic/death_music.mp3";

                //Video
                this.VideoDir = $"{tieFighterPath}/Videos";
                this.IntroVideo = $"{VideoDir}/GameVideos/intro.mp4";

                //Images
                this.ImageDir = $"{tieFighterPath}/Images";
                this.CrosshairDir = $"{ImageDir}/Crosshairs";
            }
        }
    }
}
