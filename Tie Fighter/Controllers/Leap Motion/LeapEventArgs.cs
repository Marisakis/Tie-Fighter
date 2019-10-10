using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Controllers.Leap_Motion
{
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
