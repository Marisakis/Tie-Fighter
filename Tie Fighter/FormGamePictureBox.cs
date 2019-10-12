using System;
using System.Windows.Forms;
using Tie_Fighter.Controllers;
using Leap;
using Tie_Fighter.Controllers.Leap_Motion;
using System.Drawing;
using Tie_Fighter.GameObjects.HUD;
using Tie_Fighter.GameObjects.Crosshairs;
using System.Threading;
using Tie_Fighter.Others;
using System.Diagnostics;

namespace Tie_Fighter
{
    public partial class FormGamePictureBox : Form, IActionInput<int>//, ILeapEventDelegate
    {
        //Media player
        private Others.MediaPlayerHandler _mediaPlayerHandler;

        //Event classes
        private Keyboard<KeyEventArgs> _keyboard;
        private Mouse<MouseEventArgs> _mouse;
        private LeapMotionHandler<LeapEventArgs> _leapMotion;
        //Leap
        private LeapMotion _leap;
        private Cockpit cockpit;

        //Static elements
        private Wallpaper _wallpaper;
        private Crosshair _crosshair;
        private DirectoryManager _directoryManager;

        public FormGamePictureBox()
        {
            //Init - standard forms method
            InitializeComponent();

            //Double buffering is needed to prevent flickering.
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //Create directory manager and media player.
            this._directoryManager = new DirectoryManager();
            this._mediaPlayerHandler = new Others.MediaPlayerHandler();

            //Launch video
            this._mediaPlayerHandler.PlayVideo(this._directoryManager.IntroVideo);

            //Events
            this._keyboard = new Keyboard<KeyEventArgs>(this);
            this._mouse = new Mouse<MouseEventArgs>(this);
            this._leapMotion = new LeapMotionHandler<LeapEventArgs>(this);

            //Leap
            this._leap = new LeapMotion(this);

            //GameObjects
            this.cockpit = new Cockpit(this._mediaPlayerHandler, 0, 0, 100, 100);
            this._wallpaper = new Wallpaper(this._mediaPlayerHandler, 0, 0, 100, 100);
            this._crosshair = new Crosshair(this._mediaPlayerHandler, Color.Red, 0, 0, 10, 10);

            //Create game loop
            CreateTimer();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            DrawPlayerCrosshair(graphics);
            DrawCockpit(graphics);
            base.OnPaint(e);
        }

        public void CreateTimer()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (5); // in ms
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        public void DrawBG(Graphics graphics)
        {
            this._wallpaper.Draw(graphics, Width, Height);
        }

        public void DrawCockpit(Graphics graphics)
        {
             this.cockpit.Draw(graphics, Width, Height);
        }

        public void DrawPlayerCrosshair(Graphics graphics)
        {
            this._crosshair.Draw(graphics, Width, Height, true);
        }

        public void Fire()
        {
            this._mediaPlayerHandler.PlayFile(_directoryManager.FireSound, null);
        }

        public void MoveTo(int x, int y)
        {
            this._crosshair.SetXY(x, y, Width, Height);
        }

        public void UpdatePosition(int x, int y)
        {
            this._crosshair.percentageX += x;
            this._crosshair.percentageY += y;
        }

        public void FormGame_LeapEvent(LeapEventArgs e)
        {
            _leapMotion.Action(e);
        }

        private void FormGamePictureBox_Load(object sender, EventArgs e)
        {

        }

        private void FormGamePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            _mouse.Action(e);
        }

        private void FormGamePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            _mouse.Action(e);
        }

        private void FormGamePictureBox_KeyDown(object sender, KeyEventArgs e)
        {
           _keyboard.Action(e);
        }
    }
}
