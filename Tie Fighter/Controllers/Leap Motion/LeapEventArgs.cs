using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Controllers.Leap_Motion
{
    /// <summary>
    /// Such as KeyboardEventArgs exist, and MouseEventArgs, we created the LeapEventArgs, which is 100% compatible with EventArgs and our GeneralController.
    /// </summary>
    public class LeapEventArgs : EventArgs
    {
        /// <summary>
        /// Can get / set x position of the hand tracking.
        /// </summary>
        public float x
        {
            get; set;
        }
        /// <summary>
        /// Can get / set y position of the hand tracking.
        /// </summary>
        public float y
        {
            get; set;
        }
        /// <summary>
        /// Defines whether a user has tapped (true) or not (false).
        /// </summary>
        public bool tapped
        {
            get; set;
        }
    }
}
