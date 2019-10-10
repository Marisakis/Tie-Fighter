using System;
using System.Windows.Forms;
using Tie_Fighter.Controllers;
using Leap;
using Tie_Fighter.Controllers.Leap_Motion;
using System.Drawing;
using Tie_Fighter.GameObjects.HUD;
using Tie_Fighter.GameObjects.Crosshairs;
using System.Threading;

namespace Tie_Fighter
{
    public partial class FormGamePictureBox : Form, IActionInput<int>//, ILeapEventDelegate
    {
        private Others.MediaPlayer mediaPlayer;
        private Keyboard<KeyEventArgs> _keyboard;
        private Mouse<MouseEventArgs> _mouse;
        private LeapMotionHandler<LeapEventArgs> _leapMotion;
        private Cockpit cockpit;
        private Wallpaper wallpaper;
        //--For testing purposes:
        private Crosshair _crosshair;

        private Graphics graphics;
        public FormGamePictureBox()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.mediaPlayer = new Others.MediaPlayer();
            this._keyboard = new Keyboard<KeyEventArgs>(this);
            this._mouse = new Mouse<MouseEventArgs>(this);
            this._leapMotion = new LeapMotionHandler<LeapEventArgs>(this);
          // LeapMotion leapMotion = new LeapMotion(this);
            this.cockpit = new Cockpit(this.mediaPlayer, 0, 0, 100, 100);
            this.wallpaper = new Wallpaper(this.mediaPlayer, 0, 0, 100, 100);
            this._crosshair = new Crosshair(mediaPlayer, Color.Red, 0, 0, 10, 10);

            this.graphics= this.CreateGraphics();

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
            timer.Interval = (10); // in ms
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Refresh();

           // DrawCockpit(graphics);
        }

        public void DrawBG(Graphics graphics)
        {
            this.wallpaper.Draw(graphics, Width, Height);
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
            string path2 = $@"{Application.StartupPath}\TieFighterShooterMP3\TieFighter\tie_fire.mp3";
            this.mediaPlayer.PlayFile(path2);
        }

        public void MoveTo(int x, int y)
        {
            this._crosshair.SetXY(x, y, Width, Height);
        }

        public void UpdatePosition(int x, int y)
        {
            this._crosshair.percentageX += x;
            this._crosshair.percentageY += y;
         //     DrawPlayerCrosshair(graphics);
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
