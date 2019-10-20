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
using System.Threading;

namespace Tie_Fighter
{
    /// <summary>
    /// Handles the drawing and receiving of items. This Form binds everything together.
    /// </summary>
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
        private int millis = 0;
        private FormQueue lobby;
        private string myName;

        //Locking the list
        object _lockObj = new object();
        private delegate void SafeCallDelegate(Client client, string name);
        private delegate void SafeDisposeDelegate();

        /// <summary>
        /// Create the game Forms window.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
        /// <param name="sender"></param>
        public FormGame(Client client, string name, FormQueue sender)
        {
            this.lobby = sender;
            this.myName = name;
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
        /// <summary>
        /// Windows Forms paint event. Used to draw objects and elements.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            bool _lockWasTaken = false;
            try
            {
                System.Threading.Monitor.Enter(_lockObj, ref _lockWasTaken);
                Graphics graphics = e.Graphics;


                //Draw each Tie Fighter / Explosion / Crosshair.
                foreach (Fighter tieFighter in _tieFighters)
                    tieFighter.Draw(graphics, Width, Height, true);
                foreach (Explosion explosion in _explosions)
                    explosion.Draw(graphics, Width, Height, true);
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
        /// <summary>
        /// Create the game loop draw timer.
        /// </summary>
        public void CreateTimer()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (20); // in ms
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        /// <summary>
        /// Refresh upon each timer tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        /// <summary>
        /// Draw a background wallpaper.
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawBG(Graphics graphics)
        {
            this._wallpaper.Draw(graphics, Width, Height);
        }
        /// <summary>
        /// Draw the cockpit image HUD.
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawCockpit(Graphics graphics)
        {
            this.cockpit.Draw(graphics, Width, Height);
        }
        /// <summary>
        /// Draw the player crosshair.
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawPlayerCrosshair(Graphics graphics)
        {
            this._crosshair.Draw(graphics, Width, Height, true);
        }
        /// <summary>
        /// Fire, play sound and update server data.
        /// </summary>
        public void Fire()
        {
            this._mediaPlayerHandler.PlayFile(_directoryManager.FireSound, null);
            UpdateCrosshairData(true);
        }
        /// <summary>
        /// Move crosshair to specific x,y value and update on the server.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveTo(int x, int y)
        {
            this._crosshair.SetXY(x, y, Width, Height);
            UpdateCrosshairData();
        }
        /// <summary>
        /// Update the x,y value and update on the server.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void UpdatePosition(int x, int y)
        {
            this._crosshair.percentageX += x;
            this._crosshair.percentageY += y;

            UpdateCrosshairData();
        }

        /// <summary>
        /// Send a crosshair location update to the server.
        /// </summary>
        /// <param name="isFiring"></param>
        public void UpdateCrosshairData(bool isFiring = false)
        {
            if (Math.Abs(millis - DateTime.Now.Millisecond) > 15 || isFiring)
            {

                dynamic updatePos = new JObject();
                dynamic data = new JObject();
                updatePos.type = "crosshair";
                data.x = _crosshair.percentageX;
                data.y = _crosshair.percentageY;
                data.isFiring = isFiring;
                updatePos.data = data;
                client.Write(updatePos);

                millis = DateTime.Now.Millisecond;
            }
        }

        /// <summary>
        /// Is used to call a Leap Motion input event.
        /// </summary>
        /// <param name="e"></param>
        public void FormGame_LeapEvent(LeapEventArgs e)
        {
            _leapMotion.Action(e);
        }

        /// <summary>
        /// Upon load this event is called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGamePictureBox_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Upon mouse click _mouse.Action is called for further handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGamePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            _mouse.Action(e);
        }
        /// <summary>
        /// Upon mouse move _mouxe.Action is called for further handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGamePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            _mouse.Action(e);
        }
        /// <summary>
        /// Upon key down _keyboard.Action is called for further handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGamePictureBox_KeyDown(object sender, KeyEventArgs e)
        {
            _keyboard.Action(e);
        }
        /// <summary>
        /// Find a specific player by their ID in the data received.
        /// </summary>
        /// <param name="playerID"></param>
        /// <returns></returns>
        public Player FindPlayerByID(int playerID)
        {
            foreach (Player player in players)
                if (player.id == playerID)
                    return player;
            return null;
        }
        /// <summary>
        /// Find a specific fighter by their ID.
        /// </summary>
        /// <param name="fighterID"></param>
        /// <returns></returns>
        public TieFighter FindFighterByID(int fighterID)
        {
            foreach (TieFighter fighter in _gameObjects)
                if (fighter.id == fighterID)
                    return fighter;
            return null;
        }
        /// <summary>
        /// Handle a server packet, read gameData.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sender"></param>
        public void handlePacket(dynamic data, Client sender)
        {
            switch((string)data.type)
            {
                case "gamedata":
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
                        HandleExplosions(explosions);
                        //HandlePlayers(players);
                    }
                    finally
                    {
                        if (_lockWasTaken) System.Threading.Monitor.Exit(_lockObj);
                    }
                    break;
                case "gameended":
                    Console.WriteLine("Game ended");
                    lobby.Restart(this.client, this.myName);
                    this.End();
                    //this.Dispose(); //generates thread error
                    break;
            }
            
        }
        /// <summary>
        /// End method is used to safely dispose this.
        /// </summary>
        private void End()
        {
            if(this.InvokeRequired)
            {
                var d = new SafeDisposeDelegate(End);
                this.Invoke(d);
                    
            }
            else
            {
                this.Dispose();
            }
        }
        /// <summary>
        /// Handle new / removed explosions.
        /// </summary>
        /// <param name="toAddExplosions"></param>
        /// <param name="existingExplosions"></param>
        public void HandleGameExplosions(Explosion[] toAddExplosions, List<Explosion> existingExplosions)
        {
            // If id is not in toAddExplosions, but is in existingExplosions, remove new Explosion.
            for (int i = 0; i < existingExplosions.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < toAddExplosions.Length; j++)
                    if (existingExplosions[i].id == toAddExplosions[j].id)
                        found = true;
                if (!found)
                    existingExplosions[i].Dispose();
            }
            for (int i = existingExplosions.Count - 1; i > -1; i--)
                if (existingExplosions[i].disposed)
                    existingExplosions.Remove(existingExplosions[i]);

            // If id is not in existingExplosions, but is in toAddExplosions, create new Explosion.
            List<Explosion> toAdd = new List<Explosion>();
            for (int i = 0; i < toAddExplosions.Length; i++)
            {
                bool found = false;
                for (int j = 0; j < existingExplosions.Count; j++)
                    if (toAddExplosions[i].id == existingExplosions[j].id)
                    {
                        found = true;
                        existingExplosions[j].percentageX = toAddExplosions[i].percentageX;
                        existingExplosions[j].percentageY = toAddExplosions[i].percentageY;
                        existingExplosions[j].TTL = existingExplosions[i].TTL;
                    }
                if (!found)
                    toAdd.Add(toAddExplosions[i]);
            }
            foreach (Explosion gameObject in toAdd)
            {
                existingExplosions.Add(gameObject);
                existingExplosions[existingExplosions.Count - 1].PlayExplosionSound(_directoryManager.TieExplodeSound);
            }
        }
        /// <summary>
        /// Handle new / removed GameObjects.
        /// </summary>
        /// <param name="toAddObjects"></param>
        /// <param name="existingObjects"></param>
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
                {
                    existingObjects[i].StopMediaPlayer();
                    existingObjects[i].Dispose();
                }
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
        /// <summary>
        /// Handle new / removed Player objects.
        /// </summary>
        /// <param name="players"></param>
        public void HandlePlayerObjects(Player[] players)
        {
            byte crosshairID = 0;
            List<Crosshair> AddCrosshairList = new List<Crosshair>();
            foreach (Player player in players)
            {
                foreach (Crosshair existingCrosshair in _crosshairs)
                {
                    if (player.id == existingCrosshair.id)
                    {
                        existingCrosshair.percentageX = player.x;
                        existingCrosshair.percentageY = player.y;
                    }
                    else if (crosshairID < 10)
                    {
                        Crosshair crosshair = new Crosshair(this._mediaPlayerHandler, _directoryManager.Crosshair(++crosshairID), player.x, player.y, player.w, player.h);
                        crosshair.id = player.id;
                        AddCrosshairList.Add(crosshair);
                        Console.WriteLine($"Added crosshair with x,y,w,h {player.x},{player.y},{player.w},{player.h}");
                    }
                }
            }
            foreach (Crosshair ToAddCrosshair in AddCrosshairList)
            {
                this._crosshairs.Add(ToAddCrosshair);
            }
        }
        /// <summary>
        /// Handle Fighters.
        /// </summary>
        /// <param name="fighters"></param>
        public void HandleFighters(Fighter[] fighters)
        {
            if (_tieFighters != null)
                HandleGameObjects(fighters, _tieFighters);
        }

        /// <summary>
        /// Handle Explosions.
        /// </summary>
        /// <param name="explosions"></param>
        public void HandleExplosions(Explosion[] explosions)
        {
            if (_explosions != null)
                HandleGameExplosions(explosions, _explosions);
        }
        /// <summary>
        /// Handle Players.
        /// </summary>
        /// <param name="players"></param>
        public void HandlePlayers(Player[] players)
        {
            if (this.players != null)
                if (players.Length > 1)
                    HandlePlayerObjects(players);
        }
        /// <summary>
        /// Decode a JSON Array to a class Fighter array that can easily be read.
        /// </summary>
        /// <param name="jFighters"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Decode a JSON array to a class Explosion array that can easily be read.
        /// </summary>
        /// <param name="jExplosions"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Decode a  JSON array to a class Player array that can easily be read.
        /// </summary>
        /// <param name="jPlayers"></param>
        /// <returns></returns>
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
                    JObject jCrosshair = (JObject)jPlayer.GetValue("crosshair");
                    int x = (int)jCrosshair.GetValue("x");
                    int y = (int)jCrosshair.GetValue("y");
                    int w = (int)jCrosshair.GetValue("width");
                    int h = (int)jCrosshair.GetValue("height");

                    Player player = new Player(name, score);
                    player.id = id;
                    player.x = x;
                    player.y = y;
                    player.w = w;
                    player.h = h;
                    players[i] = player;
                }
                return players;
            }
            return null;
        }

        public void HandleError(Client client)
        {
            /*FormLogin login = new FormLogin();
            login.Show();
            this.Hide();*/
        }
    }
}
