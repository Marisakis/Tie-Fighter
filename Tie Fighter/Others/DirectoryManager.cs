using System;
using System.IO;

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
                LeapDir = $"{tieFighterPath}/Leap";

                //Audio
                AudioDir = $"{tieFighterPath}/Audio";
                FireSound = $"{AudioDir}/Player/tie_fire.mp3";
                TieFighterFlyBy = $"{AudioDir}/TieFighter/flyby2.mp3";
                TieExplodeSound = $"{AudioDir}/TieFighter/explode.wav";
                Good = $"{AudioDir}/GameMusic/goodsmall.mp3";
                Bad = $"{AudioDir}/GameMusic/death_music.mp3";

                //Video
                VideoDir = $"{tieFighterPath}/Videos";
                IntroVideo = $"{VideoDir}/GameVideos/intro.mp4";

                //Images
                ImageDir = $"{tieFighterPath}/Images";
                CrosshairDir = $"{ImageDir}/Crosshairs";
            }
        }
    }
}
