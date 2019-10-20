using System;
using Leap;
using Tie_Fighter.Controllers.Leap_Motion;
namespace Tie_Fighter.Controllers
{
    public class LeapMotionHandler<LeapMotionEvent> : GeneralController<LeapMotionEvent, int> where LeapMotionEvent : LeapEventArgs
    {
        /// <summary>
        /// The LeapMotionHandler extends the GeneralController, this class handles the Leap actions.
        /// </summary>
        /// <param name="actionInput"></param>
        public LeapMotionHandler(IActionInput<int> actionInput) : base(actionInput) 
        {
           
        }

        public override void Action(LeapMotionEvent eventData)
        {
            base.actionInput.MoveTo((int)eventData.x, (int)eventData.y);
            if (eventData.tapped)
            {
                base.actionInput.Fire();
            }
        }

      
    }
}
