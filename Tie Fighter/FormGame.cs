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
using Tie_Fighter.GameObjects;
using Tie_Fighter.GameObjects.Fighters;
using System.Collections.Generic;
using Networking;
using Newtonsoft.Json.Linq;
using Tie_Fighter.Players;
using Newtonsoft.Json;

namespace Tie_Fighter
{
    public partial class FormGame : Form, IActionInput<int>, IDataReceiver //, ILeapEventDelegate
    {
        //Networking
        private Client client;

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
        private List<GameObject> _gameObjects;
        private List<Player> players;

        public FormGame(Client client)
        {
            this.client = client;
            client.SetDataReceiver(this);

            //Init - standard forms method
            InitializeComponent();

            //Double buffering is needed to prevent flickering.
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //Create directory manager and media player.
            this._directoryManager = new DirectoryManager();
            this._mediaPlayerHandler = new Others.MediaPlayerHandler();

            //Events
            this._keyboard = new Keyboard<KeyEventArgs>(this);
            this._mouse = new Mouse<MouseEventArgs>(this);
            this._leapMotion = new LeapMotionHandler<LeapEventArgs>(this);

            //Leap
            this._leap = new LeapMotion(this);

            //GameObjects
            this.cockpit = new Cockpit(this._mediaPlayerHandler, 0, 0, 100, 100);
            this._wallpaper = new Wallpaper(this._mediaPlayerHandler, 0, 0, 100, 100);

            string crosshairURL = _directoryManager.Crosshair(0);
            this._crosshair = new Crosshair(this._mediaPlayerHandler, crosshairURL, 0, 0, 10, 10);

            this._gameObjects = new List<GameObject>();
            this.players = new List<Player>();


            //Create game loop
            CreateTimer();

            //Hide cursor
            Cursor.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            DrawPlayerCrosshair(graphics);
            DrawCockpit(graphics);

            //Draw each Tie Fighter / Explosion / Crosshair.
            foreach (GameObject gameObject in _gameObjects)
                gameObject.Draw(graphics, Width, Height, true);


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

        public Player FindPlayerByID(int playerID)
        {
            foreach (Player player in players)
                if (player.id == playerID)
                    return player;
            return null;
        }

        public TieFighter FindFighterByID(int fighterID)
        {
            foreach (TieFighter fighter in _gameObjects)
         
                if (fighter.id == fighterID)
                    return fighter;
            return null;
        }

        public void handlePacket(dynamic data, Client sender)
        {
            Console.WriteLine(data);
            lock(_gameObjects)
            {

                JArray jFighters = data.fighters;
                JArray jExplosions = data.explosions;
                JArray jPlayers = data.players;

                /*for (int i = 0; i < jPlayers.Count; i++)
                {
                    dynamic jPlayer = jPlayers[i];
                    Player player = FindPlayerByID(jPlayer.id);
                    player.UpdateScore((int)jPlayer.score);
                    //update Crosshairs here!
                    Console.WriteLine($"Name: {jPlayer.name}, Score: {jPlayer.score}");
                }
*/
                for ( int i = 0; i < jFighters.Count; i++)
                {
                    dynamic jFighter = jFighters[i];
                    TieFighter fighter = FindFighterByID((int)jFighter.id);
                    if (fighter != null)
                    {
                        fighter.percentageX = (int)jFighter.x;
                        fighter.percentageY = (int)jFighter.y;
                    }
                    else
                    {
                        TieFighter newFighter = new TieFighter(this._mediaPlayerHandler, (int)jFighter.x, (int)jFighter.y, (int)jFighter.width, (int)jFighter.height);
                        _gameObjects.Add(newFighter);
                    }
                    //if fighter is known by client but not by server, remove it
                    

                }
                /*List<Player> players = jPlayers.ToObject<List<Player>>();
                List<Player> players = JArray.ToObject<List<Player>>(jPlayers);*/

            }
        }
    }
}
