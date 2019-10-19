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
using Tie_Fighter.GameObjects.Explosions;

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

        //Game objects
        private List<GameObject> _gameObjects;
        private List<Fighter> _tieFighters;
        private List<Explosion> _explosions;
        private List<Crosshair> _crosshairs;

        private List<Player> players;

        //Locking the list
        object _lockObj = new object();


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

            //GameObjects
            this._gameObjects = new List<GameObject>();
            this._tieFighters = new List<Fighter>();
            this._explosions = new List<Explosion>();
            this._crosshairs = new List<Crosshair>();

            this.players = new List<Player>();


            //Create game loop
            CreateTimer();

            //Hide cursor
            Cursor.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            bool _lockWasTaken = false;
            try
            {
                System.Threading.Monitor.Enter(_lockObj, ref _lockWasTaken);
                Graphics graphics = e.Graphics;


                //Draw each Tie Fighter / Explosion / Crosshair.
                foreach (Fighter tieFighter in _tieFighters)
                    tieFighter.Draw(graphics, Width, Height, false);
                foreach (Explosion explosion in _explosions)
                    explosion.Draw(graphics, Width, Height, false);
                foreach (Crosshair crosshair in _crosshairs)
                    crosshair.Draw(graphics, Width, Height, true);

                DrawPlayerCrosshair(graphics);
                DrawCockpit(graphics);
                base.OnPaint(e);
            }
            finally
            {
                if (_lockWasTaken) System.Threading.Monitor.Exit(_lockObj);
            }
        }

        public void CreateTimer()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (20); // in ms
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

            //Build data packet to write to server containing player position.
            dynamic updatePos = new JObject();
            updatePos.type = "crosshairUpdate";
            updatePos.x = x;
            updatePos.y = y;
           // client.Write(updatePos);
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
            JArray jFighters = data.fighters;
            JArray jExplosions = data.explosions;
            JArray jPlayers = data.players;

            Fighter[] fighters = GetFighters(jFighters);
            Explosion[] explosions = GetExplosions(jExplosions);
            Player[] players = GetPlayers(jPlayers);

            bool _lockWasTaken = false;
            try
            {
                System.Threading.Monitor.Enter(_lockObj, ref _lockWasTaken);
                HandleFighters(fighters);
            }
            finally
            {
                if (_lockWasTaken) System.Threading.Monitor.Exit(_lockObj);
            }
        }

        

        public void HandleGameObjects(Fighter[] toAddObjects, List<Fighter> existingObjects)
        {
            // If id is not in toAddObjects, but is in existingObjects, remove new GameObject.
            for (int i = 0; i < existingObjects.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < toAddObjects.Length; j++)
                    if (existingObjects[i].id == toAddObjects[j].id)
                        found = true;
                if (!found)
                    existingObjects[i].Dispose();
            }
            for (int i = existingObjects.Count - 1; i > -1; i--)
                if (existingObjects[i].disposed)
                    existingObjects.Remove(existingObjects[i]);

            // If id is not in existingObjects, but is in toAddObjects, create new GameObject.
            List<Fighter> toAdd = new List<Fighter>();
            for (int i = 0; i < toAddObjects.Length; i++)
            {
                bool found = false;
                for (int j = 0; j < existingObjects.Count; j++)
                    if (toAddObjects[i].id == existingObjects[j].id)
                    {
                        found = true;
                        existingObjects[j].percentageX = toAddObjects[i].percentageX;
                        existingObjects[j].percentageY = toAddObjects[i].percentageY;
                        existingObjects[j].TTP = toAddObjects[i].TTP;
                    }
                if (!found)
                    toAdd.Add(toAddObjects[i]);
            }
            foreach (Fighter gameObject in toAdd)
            {
                existingObjects.Add(gameObject);
                existingObjects[existingObjects.Count - 1].PlayFlySound(_directoryManager.TieFighterFlyBy);
                if (existingObjects[existingObjects.Count - 1].TTP < 3000) existingObjects[existingObjects.Count - 1].MakeTieInterceptor();
                if (existingObjects[existingObjects.Count - 1].TTP > 6000) existingObjects[existingObjects.Count - 1].MakeTieBomber();
            }
        }

        public void HandleFighters(Fighter[] fighters)
        {
            if (_tieFighters != null)
                HandleGameObjects(fighters, _tieFighters);
        }

        public void HandleExplosions(Explosion[] explosions)
        {

        }

        public void HandlePlayers(Player[] players)
        {

        }

        public Fighter[] GetFighters(JArray jFighters)
        {
            Fighter[] fighters;
            if (jFighters != null)
            {
                fighters = new Fighter[jFighters.Count];
                for (int i = 0; i < jFighters.Count; i++)
                {
                    JObject jFighter = jFighters[i].ToObject<JObject>();
                    int id = (int)jFighter.GetValue("id");
                    int x = (int)jFighter.GetValue("x");
                    int y = (int)jFighter.GetValue("y");
                    int width = (int)jFighter.GetValue("width");
                    int height = (int)jFighter.GetValue("height");
                    int TTP = (int)jFighter.GetValue("TTP");
                    Fighter fighter = new TieFighter(this._mediaPlayerHandler, x, y, width, height);
                    fighter.TTP = TTP;
                    fighter.id = id;
                    fighters[i] = fighter;
                }
                return fighters;
            }
            return null;
        }

        public Explosion[] GetExplosions(JArray jExplosions)
        {
            Explosion[] explosions;
            if (jExplosions != null)
            {
                explosions = new Explosion[jExplosions.Count];
                for (int i = 0; i < jExplosions.Count; i++)
                {
                    JObject jExplosion = jExplosions[i].ToObject<JObject>();
                    int id = (int)jExplosion.GetValue("id");
                    int x = (int)jExplosion.GetValue("x");
                    int y = (int)jExplosion.GetValue("y");
                    int width = (int)jExplosion.GetValue("width");
                    int height = (int)jExplosion.GetValue("height");
                    int TTL = (int)jExplosion.GetValue("TTL");
                    Explosion explosion = new Explosion(this._mediaPlayerHandler, x, y, width, height);
                    explosion.TTL = TTL;
                    explosion.id = id;
                    explosions[i] = explosion;
                }
                return explosions;
            }
            return null;
        }

        public Player[] GetPlayers(JArray jPlayers)
        {
            Player[] players;
            if (jPlayers != null)
            {
                players = new Player[jPlayers.Count];
                for (int i = 0; i < jPlayers.Count; i++)
                {
                    JObject jPlayer = jPlayers[i].ToObject<JObject>();
                    int id = (int)jPlayer.GetValue("id");
                    string name = (string)jPlayer.GetValue("string");
                    int score = (int)jPlayer.GetValue("score");
                    Player player = new Player(name, score);
                    player.id = id;
                    players[i] = player;
                }
                return players;
            }
            return null;
        }
    }
}
