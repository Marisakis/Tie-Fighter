using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace Tie_Fighter.Controllers.Leap_Motion
{
    public class LeapMotion : ILeapEventDelegate
    {
        private Controller _controller;
        private LeapEventListener _listener;
        private FormGame _formGame;

        public LeapMotion(FormGame formGame) // FormGame in constructor could become an interface later.
        {
            this._formGame = formGame;
            this._controller = new Controller();
            this._listener = new LeapEventListener(this);
            this._controller.AddListener(_listener);
        }
        delegate void LeapEventDelegate(string EventName);
        public void LeapEventNotification(string EventName)
        {
            if (!_formGame.InvokeRequired)
            {
                switch (EventName)
                {
                    case "onInit":
                        Console.WriteLine("Intialized Leap Motion");
                        break;
                    case "onConnect":
                        Console.WriteLine("Connected Leap Motion");
                        break;
                    case "onFrame":
                        Console.WriteLine("Received frame Leap Motion");
                        break;
                }
            }
            else
            {
                _formGame.BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });
            }
        }

    }
}
