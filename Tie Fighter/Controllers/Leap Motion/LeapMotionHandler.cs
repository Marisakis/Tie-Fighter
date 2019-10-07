using System;
using Leap;
using Tie_Fighter.Controllers.Leap_Motion;
namespace Tie_Fighter.Controllers
{
    public class LeapMotionHandler<LeapMotionEvent, T> : GeneralController<LeapMotionEvent, T>  // where LeapMotionEvent : [eventclass]
    {
        
        public LeapMotionHandler(IActionInput<T> actionInput) : base(actionInput) 
        {
           
        }

        public override void Action(LeapMotionEvent eventData)
        {
            throw new NotImplementedException();
        }

      
    }
}
