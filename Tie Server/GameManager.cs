using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Tie_Server.GameObjects;

namespace Tie_Server
{

    public class GameManager
    {
        public const int timerPeriod = 50; //Time in millisecond between each internal update
        private List<Target> tieFighters;
        private List<Explosion> explosions;
        public List<Player> players;
        private int targetCounter = 0;
        private Random randomSeederForTieFighters = new Random();

        public GameManager()
        {
            tieFighters = new List<Target>();
            explosions = new List<Explosion>();
            players = new List<Player>();

            var timerDelegate = new System.Timers.Timer(timerPeriod); 
            timerDelegate.Elapsed += OnTimedEvent;
            timerDelegate.AutoReset = true;
            timerDelegate.Enabled = true;

        }

        public dynamic GetGameData()
        {
            lock (this)
            {
                JArray jFighters = JArray.FromObject(tieFighters);
                JArray jExplosions = JArray.FromObject(explosions);
                JArray jPlayers = JArray.FromObject(players);
                dynamic data = new JObject();
                data.fighters = jFighters;
                data.explosions = jExplosions;
                data.player = jPlayers;
                return data;
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                UpdateTieFighters();
                UpdateExplosions();
                if (SpawnRandomTieFighter(50))
                    CreateNewFighters();
                CheckCrosshairHits();
            }
        }

        public bool SpawnRandomTieFighter(int maxOdd)  // between 0 and maxOdd.
        {
            Random random = new Random();
            int outcome = random.Next(0, maxOdd);
            return (outcome == 1);
        }

        public int GetRandomHeightTieFighter()
        {
            return randomSeederForTieFighters.Next(10, 90);
        }

        internal void UpdatePlayerCrosshair(dynamic clientID, dynamic crosshair)
        {
            int playerID = clientID;
            int x = crosshair.x;
            int y = crosshair.y;
            bool isFiring = crosshair.isFiring;

            Player player = FindPlayerByID(playerID);
            if (player != null)
                player.UpdateCrosshair(x, y, isFiring);
        }

        public Player FindPlayerByID(int playerID)
        {
            foreach (Player player in players)
                if (player.id == playerID)
                    return player;
            return null;
        }


        /// <summary>
        /// This method moves all existing TieFighters over the screen. It also checks for fighters that have exceeded screen boundaries
        /// </summary>
        private void UpdateTieFighters()
        {
            Debug.WriteLine("Handling " + tieFighters.Count + " fighters");
            var toRemove = new List<Target>();
            foreach (Target t in tieFighters)
            {
                t.x += (100.0 / (t.TTP / timerPeriod));
                if (t.x > 100)
                    toRemove.Add(t);
            }
            foreach (Target t in toRemove)
                tieFighters.Remove(t);
        }

        /// <summary>
        /// This method reduces the Time To Live of each existing Explosion, removing them when TTL reaches 0;
        /// </summary>
        private void UpdateExplosions()
        {
            var toRemove = new List<Explosion>();
            foreach (Explosion x in explosions)
            {
                x.TTL -= 20;
                if (x.TTL < 0)
                    toRemove.Add(x);
            }
            foreach (Explosion x in toRemove)
                explosions.Remove(x);
        }

        /// <summary>
        /// This method checks if the amount of fighters is beneath a certain trehshold and adds one if needed
        /// </summary>
        private void CreateNewFighters()
        {
            if (tieFighters.Count < 5)
                tieFighters.Add(new Target((randomSeederForTieFighters.Next(4, 80)*100), targetCounter++, 0, GetRandomHeightTieFighter(), 10, 10)); // id management not in yet
        }

        /// <summary>
        /// This method checks if any of the crosshairs are currently firing and over the position of an existing fighter
        /// If so, an explosion is created and score is awarded
        /// </summary>
        private void CheckCrosshairHits()
        {
            //Check each crosshair with each tie fighter
            // if hit, remove fighter, increase score, add new explosion with targetcounter id
        }

    }
}
