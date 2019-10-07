using System;
using System.Windows.Forms;
using Tie_Fighter.Controllers;
using Leap;
using Tie_Fighter.Controllers.Leap_Motion;
using System.Drawing;

namespace Tie_Fighter
{
    public partial class FormGame : Form, IActionInput<int>//, ILeapEventDelegate
    {
        private Tie_Fighter.Others.MediaPlayer mediaPlayer;
        private Keyboard<KeyEventArgs> _keyboard;
        private Mouse<MouseEventArgs> _mouse;
        private LeapMotionHandler<LeapEventArgs> _leapMotion;

        public FormGame()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.mediaPlayer = new Others.MediaPlayer();
            this._keyboard = new Keyboard<KeyEventArgs>(this);
            this._mouse = new Mouse<MouseEventArgs>(this);
            this._leapMotion = new LeapMotionHandler<LeapEventArgs>(this);
            LeapMotion leapMotion = new LeapMotion(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            DrawCockpit(graphics);
            base.OnPaint(e);
        }

        public void DrawCockpit(Graphics graphics)
        {
            Bitmap backgroundImage = Properties.Resources.Cockpit;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            var rc = new Rectangle(0, 0, Width, Height);
            graphics.DrawImage(backgroundImage, rc);
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

        public void FormGame_LeapEvent(LeapEventArgs e)
        {
            _leapMotion.Action(e);
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }
    }
}
