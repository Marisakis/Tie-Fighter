using Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Timers;
using Tie_Server.GameObjects;


namespace Tie_Server
{
    /// <summary>
    /// The GameManager handles notifying clients with GameObject elements and handles the game which is running. Decides where new Fighters spawn, etc.
    /// </summary>
    public class GameManager
    {
        public const int timerPeriod = 50; //Time in millisecond between each internal update
        public const int explosionTimeToLive = 400;
        private readonly List<Target> tieFighters;
        private readonly List<Explosion> explosions;
        public List<Player> players;
        private int targetCounter = 0;
        private readonly Random randomSeederForTieFighters = new Random();
        private readonly object _lockObj = new object();

        /// <summary>
        /// Initialization and create an update timer in the default constructor.
        /// </summary>
        public GameManager()
        {
            tieFighters = new List<Target>();
            explosions = new List<Explosion>();
            players = new List<Player>();

            Timer timerDelegate = new System.Timers.Timer(timerPeriod);
            timerDelegate.Elapsed += OnTimedEvent;
            timerDelegate.AutoReset = true;
            timerDelegate.Enabled = true;

        }

        /// <summary>
        /// Receive all Game data.
        /// </summary>
        /// <returns></returns>
        public dynamic GetGameData()
        {
            lock (this)
            {
                JArray jFighters = JArray.FromObject(tieFighters);
                JArray jExplosions = JArray.FromObject(explosions);
                JArray jPlayers = JArray.FromObject(players);
                dynamic data = new JObject();
                data.type = "gamedata";
                data.fighters = jFighters;
                data.explosions = jExplosions;
                data.players = jPlayers;
                return data;
            }
        }

        /// <summary>
        /// Update tie fighters, explosions and overall game. Also check for crosshair hits and _could_ create a new Tie Fighter (randomized).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                UpdateTieFighters();
                UpdateExplosions();
                if (SpawnRandomTieFighter(35))
                {
                    CreateNewFighters();
                }

                CheckCrosshairHits();
            }
        }

        /// <summary>
        /// Spawn a random tie fighter.
        /// </summary>
        /// <param name="maxOdd"></param>
        /// <returns></returns>
        public bool SpawnRandomTieFighter(int maxOdd)  // between 0 and maxOdd.
        {
            Random random = new Random();
            int outcome = random.Next(0, maxOdd);
            return (outcome == 1);
        }

        /// <summary>
        /// Get a random TieFighter height.
        /// </summary>
        /// <returns></returns>
        public int GetRandomHeightTieFighter()
        {
            return randomSeederForTieFighters.Next(10, 90);
        }

        /// <summary>
        /// Update the crosshair position of a player.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="crosshair"></param>
        internal void UpdatePlayerCrosshair(Client client, dynamic crosshair)
        {
            int x = crosshair.x;
            int y = crosshair.y;
            bool isFiring = crosshair.isFiring;
            foreach (Player player in players)
            {
                if (player.client == client)
                {
                    player.crosshair.x = x;
                    player.crosshair.y = y;
                    player.crosshair.isFiring = isFiring;
                }
            }
        }

        /// <summary>
        /// This method moves all existing TieFighters over the screen. It also checks for fighters that have exceeded screen boundaries
        /// </summary>
        private void UpdateTieFighters()
        {
            List<Target> toRemove = new List<Target>();
            foreach (Target t in tieFighters)
            {
                t.x += (100.0 / (t.TTP / timerPeriod));
                if (t.x > 100)
                {
                    toRemove.Add(t);
                }
            }
            foreach (Target t in toRemove)
            {
                tieFighters.Remove(t);
            }
        }

        /// <summary>
        /// This method reduces the Time To Live of each existing Explosion, removing them when TTL reaches 0;
        /// </summary>
        private void UpdateExplosions()
        {
            List<Explosion> toRemove = new List<Explosion>();
            foreach (Explosion x in explosions)
            {
                x.TTL -= 20;
                if (x.TTL < 0)
                {
                    toRemove.Add(x);
                }
            }
            foreach (Explosion x in toRemove)
            {
                explosions.Remove(x);
            }
        }

        /// <summary>
        /// This method checks if the amount of fighters is beneath a certain trehshold and adds one if needed
        /// </summary>
        private void CreateNewFighters()
        {
            if (tieFighters.Count < 5)
            {
                tieFighters.Add(new Target((randomSeederForTieFighters.Next(4, 80) * 100), targetCounter++, 0, GetRandomHeightTieFighter(), 10, 10)); // id management not in yet
            }
        }

        /// <summary>
        /// This method checks if any of the crosshairs are currently firing and over the position of an existing fighter
        /// If so, an explosion is created and score is awarded
        /// </summary>
        private void CheckCrosshairHits()
        {
            List<Target> ToRemoveList = new List<Target>();
            foreach (Player player in players)
            {
                foreach (Target target in tieFighters)
                {
                    if (player.crosshair.isFiring)
                    {
                        if ((Math.Abs(player.crosshair.x - target.x) <= target.width / 2) && (Math.Abs(player.crosshair.y - target.y) <= target.height / 2))
                        {
                            //Handle here
                            ToRemoveList.Add(target);
                            player.crosshair.isFiring = false;
                            player.score++;
                        }
                    }
                }
            }

            bool _lockWasTaken = false;
            if (ToRemoveList.Count > 0)
            {
                try
                {
                    System.Threading.Monitor.Enter(_lockObj, ref _lockWasTaken);
                    foreach (Target ToRemove in ToRemoveList)
                    {
                        explosions.Add(new Explosion(explosionTimeToLive, targetCounter++, ToRemove.x, ToRemove.y, 5, 5));
                        tieFighters.Remove(ToRemove);
                    }
                }
                finally
                {
                    if (_lockWasTaken)
                    {
                        System.Threading.Monitor.Exit(_lockObj);
                    }
                }
            }

            ToRemoveList.Clear();
        }
    }
}
