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
        public float x
        {
            get; set;
        }
        public float y
        {
            get; set;
        }

        public bool tapped
        {
            get; set;
        }
    }
}
