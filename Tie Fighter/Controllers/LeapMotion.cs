using System;

namespace Tie_Fighter.Controllers
{
    public class LeapMotion<LeapMotionEvent, T> : GeneralController<LeapMotionEvent, T> // where LeapMotionEvent : [eventclass]
    {
        public LeapMotion(IActionInput<T> actionInput) : base(actionInput)
        {
        }

        public override void Action(LeapMotionEvent eventData)
        {
            throw new NotImplementedException();
        }
    }
}
