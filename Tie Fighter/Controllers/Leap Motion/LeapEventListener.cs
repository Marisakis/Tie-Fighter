using Leap;

namespace Tie_Fighter.Controllers.Leap_Motion
{
    /// <summary>
    /// Event listener for Leap events.
    /// </summary>
    public class LeapEventListener : Listener
    {
        private readonly ILeapEventDelegate eventDelegate;

        public LeapEventListener(ILeapEventDelegate delegateObject)
        {
            eventDelegate = delegateObject;
        }
        public override void OnInit(Controller controller)
        {
            eventDelegate.LeapEventNotification("onInit");
        }
        public override void OnConnect(Controller controller)
        {
            eventDelegate.LeapEventNotification("onConnect");
        }
        public override void OnFrame(Controller controller)
        {
            eventDelegate.LeapEventNotification("onFrame");
        }
        public override void OnExit(Controller controller)
        {
            eventDelegate.LeapEventNotification("onExit");
        }
        public override void OnDisconnect(Controller controller)
        {
            eventDelegate.LeapEventNotification("onDisconnect");
        }
    }
}
