using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Controllers.Leap_Motion
{
    /// <summary>
    /// Delegate needed for LeapMotion support in our application.
    /// </summary>
    public interface ILeapEventDelegate
    {
        void LeapEventNotification(string EventName);
    }
}
