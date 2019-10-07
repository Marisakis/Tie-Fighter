using System;
using System.Windows.Forms;
using Tie_Fighter.Controllers;
using Leap;
using Tie_Fighter.Controllers.Leap_Motion;

namespace Tie_Fighter
{
    public partial class FormGame : Form, IActionInput<int>//, ILeapEventDelegate
    {
        private Tie_Fighter.Others.MediaPlayer mediaPlayer;
        private Keyboard<KeyEventArgs> _keyboard;
        private Mouse<MouseEventArgs> _mouse;
        // private LeapMotion<LeapEventArgs> _leapMotion;

        //private Controller controller;
        //private LeapEventListener listener;

        public FormGame()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.mediaPlayer = new Others.MediaPlayer();
            this._keyboard = new Keyboard<KeyEventArgs>(this);
            this._mouse = new Mouse<MouseEventArgs>(this);
            LeapMotion leapMotion = new LeapMotion(this);
           

            //this.controller = new Controller();
            //this.listener = new LeapEventListener(this);
            //controller.AddListener(listener);
        }

        public void Fire()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path2 = $@"{Application.StartupPath}\TieFighterShooterMP3\TieFighter\tie_fire.mp3";
            //  Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            //  string pathToFile = $@"{path}\TieFighterShooterMP3\TieFighter\tie_fire.mp3";
            this.mediaPlayer.PlayFile(path2);
        }

        public void MoveTo(int x, int y)
        {
        }

        public void UpdatePosition(int x, int y)
        {
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            _keyboard.Action(e);
        }

        private void FormGame_MouseClick(object sender, MouseEventArgs e)
        {
            _mouse.Action(e);

            // Console.WriteLine("You Clicked");
        }
        /*
                //---------------LEAP MOTION CODE---------------------
                delegate void LeapEventDelegate(string EventName);
                public void LeapEventNotification(string EventName)
                {
                    if (!this.InvokeRequired)
                    {
                        switch (EventName)
                        {
                            case "onInit":
                                MessageBox.Show("Initialized Leap Motion!");
                                break;
                            case "onConnect":
                                MessageBox.Show("Connected Leap Motion!");
                                break;
                            case "onFrame":
                                MessageBox.Show("Frame Taken!");
                                break;
                        }
                    }
                    else
                    {
                        BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });
                    }
                }
                */
            }
    /*
            // Leap Motion controller code:
            public interface ILeapEventDelegate
            {
                void LeapEventNotification(string EventName);
            }

            public class LeapEventListener : Listener
            {
                ILeapEventDelegate eventDelegate;

                public LeapEventListener(ILeapEventDelegate delegateObject)
                {
                    this.eventDelegate = delegateObject;
                }
                public override void OnInit(Controller controller)
                {
                    this.eventDelegate.LeapEventNotification("onInit");
                }
                public override void OnConnect(Controller controller)
                {
                    this.eventDelegate.LeapEventNotification("onConnect");
                }
                public override void OnFrame(Controller controller)
                {
                    this.eventDelegate.LeapEventNotification("onFrame");
                }
                public override void OnExit(Controller controller)
                {
                    this.eventDelegate.LeapEventNotification("onExit");
                }
                public override void OnDisconnect(Controller controller)
                {
                    this.eventDelegate.LeapEventNotification("onDisconnect");
                }
            }
            */
    }
