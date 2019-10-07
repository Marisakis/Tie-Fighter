using System;
using Leap;
using Tie_Fighter.Controllers.Leap_Motion;
namespace Tie_Fighter.Controllers
{
    public class LeapMotionHandler<LeapMotionEvent> : GeneralController<LeapMotionEvent, int> where LeapMotionEvent : LeapEventArgs
    {
        
        public LeapMotionHandler(IActionInput<int> actionInput) : base(actionInput) 
        {
           
        }

        public override void Action(LeapMotionEvent eventData)
        {
            
            throw new NotImplementedException();
        }

      
    }
}
