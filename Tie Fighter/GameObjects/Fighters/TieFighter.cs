using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects.Fighters
{
    /// <summary>
    /// Tie Fighter class extends the Fighter class. Very specific methods for the TieFighter / TieBomber / TieInterceptor or whatever should be put in a dedicated class.
    /// </summary>
    public class TieFighter : Fighter
    {
        /// <summary>
        /// Used to display a TieFighter with very specific (non interceptor / bomber stuff). Might be used later.
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <param name="xPercentage"></param>
        /// <param name="yPercentage"></param>
        /// <param name="widthPercentage"></param>
        /// <param name="heightPercentage"></param>
        public TieFighter(Others.MediaPlayerHandler mediaPlayer, int xPercentage, int yPercentage, int widthPercentage, int heightPercentage) : base(mediaPlayer, xPercentage, yPercentage, widthPercentage, heightPercentage)
        {
            bitmap = Properties.Resources.TieFighter;
        }
    }
}
