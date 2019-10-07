using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Controllers.Leap_Motion
{
    interface ILeapMotionEvent
    {
        float x
        {
            get; set;
        }
        float y
        {
            get; set;
        }

        bool tapped
        {
            get; set;
        }
    }
}
