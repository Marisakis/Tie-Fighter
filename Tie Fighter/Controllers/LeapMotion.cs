using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Controllers
{
    public class LeapMotion<LeapMotionEvent> : GeneralController<LeapMotionEvent> // where LeapMotionEvent : [eventclass]
    {
        public override void Action(LeapMotionEvent eventData)
        {
            throw new NotImplementedException();
        }
    }
}
