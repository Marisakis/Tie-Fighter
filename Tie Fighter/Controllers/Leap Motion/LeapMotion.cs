using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leap;
using static Leap.Bone;
using static Leap.Finger;

namespace Tie_Fighter.Controllers.Leap_Motion
{
    /// <summary>
    /// This LeapMotion class handles the detection of specific gestures. Finger positions, bones and other muscle positions are being detected by the Leap Motion, which enables support for the Leap Motion.
    /// </summary>
    public class LeapMotion : ILeapEventDelegate
    {
        private Controller _controller;
        private LeapEventListener _listener;
        private FormGame _formGame;
        private LeapEventArgs _leapEventArgs = new LeapEventArgs();

        /// <summary>
        /// FormGame is needed to check whether an invoke is required. Is also used to transmit the event to the client.
        /// </summary>
        /// <param name="formGame"></param>

        public LeapMotion(FormGame formGame)
        {
            this._formGame = formGame;
            this._controller = new Controller();
            this._listener = new LeapEventListener(this);
            this._controller.AddListener(_listener);
        }

        /// <summary>
        /// The event delegate defines what the action is. For example, it can be an initialisation of the Leap, but also a connect, or a data frame.
        /// </summary>
        /// <param name="EventName"></param>
        delegate void LeapEventDelegate(string EventName);

        /// <summary>
        /// Retrieve an event (such as a hand wave) by calling gesture detection methods. If an invoke is required, BeginInvoke is called.
        /// </summary>
        /// <param name="EventName"></param>
        public void LeapEventNotification(string EventName)
        {
            //Will be true if the current thread is not the UI thread.
            if (!_formGame.InvokeRequired)
            {

                switch (EventName)
                {
                    case "onInit":
                        Console.WriteLine("Intialized Leap Motion");
                        break;
                    case "onConnect":
                        Console.WriteLine("Connected Leap Motion");
                        ConnectHandler();
                        break;
                    case "onFrame":
                        DetectGesture(this._controller.Frame());
                        DetectCoordinates(this._controller.Frame());
                        break;
                }
            }
            //Invoke the function if on the UI thread.
            else
            {
                _formGame.BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });
            }
        }

        /// <summary>
        /// Enable tap and screentap gestures for tracking. Can also enable a few other gestures, but those are not used for the TieFighter game.
        /// </summary>
        public void ConnectHandler()
        {
            this._controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            this._controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
        }

        /// <summary>
        /// Detect gestures using the Leap Motion API DLL's. Also notify the FormGame of the action by passing the LeapEventArgs as a parameter.
        /// </summary>
        /// <param name="frame"></param>
        public void DetectGesture(Frame frame)
        {
            GestureList gestures = frame.Gestures();
            for (int i =0; i < gestures.Count; i++)
            {
                Gesture gesture = gestures[i];
                switch(gesture.Type)
                {
                    case Gesture.GestureType.TYPE_CIRCLE:
                        Console.WriteLine("Detected circle");
                        _leapEventArgs.tapped = true;
                        break;
                    case Gesture.GestureType.TYPE_KEY_TAP:
                        Console.WriteLine("Detected tap");
                        _leapEventArgs.tapped = true;
                        break;
                    case Gesture.GestureType.TYPE_SWIPE:
                        Console.WriteLine("Detected swipe");
                        _leapEventArgs.tapped = true;
                        break;
                    case Gesture.GestureType.TYPE_SCREEN_TAP:
                        Console.WriteLine("Detected screen tap");
                        _leapEventArgs.tapped = true;
                        break;
                }
                _formGame.FormGame_LeapEvent(this._leapEventArgs);
                _leapEventArgs.tapped = false;
            }
        }

        /// <summary>
        /// Detect the hand position of a user.
        /// </summary>
        /// <param name="frame"></param>
        public void DetectHandPosition(Frame frame)
        {
            HandList hands = frame.Hands;
            foreach(Hand hand in hands)
            {
                float pitch = hand.Direction.Pitch;
                float yaw = hand.Direction.Yaw;
                float roll = hand.PalmNormal.Roll;

                double degreesPitch = pitch * (180 / Math.PI);
                double degreesYaw = yaw * (180 / Math.PI);
                double degreesRoll = roll * (180 / Math.PI);

                int intPitch = (int)degreesPitch;
                int intYaw = (int)degreesYaw;
                int intDegrees = (int)degreesRoll;

                float grab = hand.GrabStrength;
            }
        }

        /// <summary>
        /// Detect the finger positions of a user.
        /// </summary>
        /// <param name="frame"></param>
        public void DetectFingers(Frame frame)
        {
            FingerList fingers = frame.Fingers;
            foreach(Finger finger in fingers)
            {
                int fingerID = finger.Id;
                FingerType fingerType = finger.Type;
                float fingerLength = finger.Length;
                float fingerWidth = finger.Width;

                foreach(Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))
                {
                    Bone bone = finger.Bone(boneType);
                    BoneType trueBoneType = bone.Type;
                    float boneLength = bone.Length;
                    float boneWidth = bone.Width;
                    Vector boneJoint = bone.PrevJoint;
                    Vector nextJoint = bone.NextJoint;
                    Vector boneDirection = bone.Direction;
                }
            }
        }

        /// <summary>
        /// Detect the coordinates of a user. FormGame_LeapEvent(args) is called to update to player crosshair position.
        /// </summary>
        /// <param name="frame"></param>
        public void DetectCoordinates(Frame frame)
        {
            int appWidth = this._formGame.Width;
            int appHeight = this._formGame.Height;
            InteractionBox iBox = this._controller.Frame().InteractionBox;
            Pointable pointable = this._controller.Frame().Pointables.Frontmost;
            Vector leapPoint = pointable.StabilizedTipPosition;
            Vector normalizedPoint = iBox.NormalizePoint(leapPoint, false);
            float appX = normalizedPoint.x * appWidth;
            float appY = (1 - normalizedPoint.y) * appHeight;
            Console.WriteLine("X: "+appX+" Y: "+appY);
            _leapEventArgs.x = appX;
            _leapEventArgs.y = appY; 
            _formGame.FormGame_LeapEvent(this._leapEventArgs);
        }

    }
}
