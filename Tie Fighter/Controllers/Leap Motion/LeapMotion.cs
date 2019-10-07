using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using static Leap.Bone;
using static Leap.Finger;

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
                        ConnectHandler();
                        break;
                    case "onFrame":
                       // Console.WriteLine("Received frame Leap Motion");
                        DetectGesture(this._controller.Frame());
                        DetectHandPosition(this._controller.Frame());
                        DetectFingers(this._controller.Frame());
                        DetectCoordinates(this._controller.Frame());
                        break;
                }
            }
            else
            {
                _formGame.BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });
            }
        }

        public void ConnectHandler()
        {
            this._controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            this._controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            this._controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
            this._controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
        }

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
                        break;
                    case Gesture.GestureType.TYPE_KEY_TAP:
                        Console.WriteLine("Detected tap");
                        break;
                    case Gesture.GestureType.TYPE_SWIPE:
                        Console.WriteLine("Detected swipe");
                        break;
                    case Gesture.GestureType.TYPE_SCREEN_TAP:
                        //Console.WriteLine("Detected screen tap");
                        break;
                }
            }
        }

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

        public void DetectCoordinates(Frame frame)
        {
            int appWidth = this._formGame.Width;
            int appHeight = this._formGame.Height;
            //InteractionBox iBox = frame.InteractionBox;
            InteractionBox iBox = this._controller.Frame().InteractionBox;
            //Pointable pointable = frame.Pointables.Frontmost;
            Pointable pointable = this._controller.Frame().Pointables.Frontmost;
            Vector leapPoint = pointable.StabilizedTipPosition;
            Vector normalizedPoint = iBox.NormalizePoint(leapPoint, false);
            float appX = normalizedPoint.x * appWidth;
            float appY = (1 - normalizedPoint.y) * appHeight;
            Console.WriteLine("X: "+appX+" Y: "+appY);
           
        }

    }
}
