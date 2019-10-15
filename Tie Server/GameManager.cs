using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using System.Diagnostics;
using Tie_Server.GameObjects;

namespace Tie_Server
{

    class GameManager
    {
        private Timer updateTimer;
        private List<Target> tieFighters;
        private List<Explosion> explosions;
        //private Dictionary<int, Crosshair> crosshairs;
        private List<Player> players;

        public GameManager()
        {
            tieFighters = new List<Target>();
            explosions = new List<Explosion>();
            //crosshairs = new Dictionary<int, Crosshair>();
            players = new List<Player>();

            var timerDelegate = new System.Timers.Timer(20); // 
            timerDelegate.Elapsed += OnTimedEvent;
            timerDelegate.AutoReset = true;
            timerDelegate.Enabled = true;

        }


        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine("Processing game data");
            lock (this)
            {
                UpdateTieFighters();
                UpdateExplosions();
                CreateNewFighters();
                CheckCrosshairHits();
            }
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
                t.x += (int) ((20 / 1000.0) * 100 * t.TTP);
                if (t.x > 100)
                    toRemove.Add(t);
            }
            foreach(Target t in toRemove)
            {
                tieFighters.Remove(t);
            }
        }

        /// <summary>
        /// This method reduces the Time To Live of each existing Explosion, removing them when TTL reaches 0;
        /// </summary>
        private void UpdateExplosions()
        {
            var toRemove = new List<Explosion>();
            foreach(Explosion x in explosions)
            {
                x.TTL -= 20;
                if (x.TTL < 0)
                    toRemove.Add(x);
            }
            foreach (Explosion x in toRemove)
            {
                explosions.Remove(x);
            }
        }

        /// <summary>
        /// This method checks if the amount of fighters is beneath a certain trehshold and adds new ones if needed
        /// </summary>
        private void CreateNewFighters()
        {
            if (tieFighters.Count < 10)
            {
                tieFighters.Add(new Target(3, 16, 0, 100, 50, 50)); // id management not in yet
            }
        }

        /// <summary>
        /// This method checks if any of the crosshairs are currently firing and over the position of an existing fighter
        /// If so, an explosion is created and score is awarded
        /// </summary>
        private void CheckCrosshairHits()
        {

        }

    }
}
