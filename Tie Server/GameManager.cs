using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tie_Fighter.GameObjects.Fighters;
using Tie_Fighter.GameObjects.Explosions;
using Tie_Fighter.GameObjects.Crosshairs;

namespace Tie_Server
{

    class GameManager
    {
        private Timer updateTimer;
        private List<TieFighter<double>> tieFighters;
        private List<Explosion<double>> explosions;
        private Dictionary<int, Crosshair<double>> crosshairs;

        public GameManager()
        {
            tieFighters = new List<TieFighter<double>>();
            explosions = new List<Explosion<double>>();
            crosshairs = new Dictionary<int, Crosshair<double>>();

            var timerDelegate = new System.Timers.Timer(20); // 
            timerDelegate.Elapsed += OnTimedEvent;
            timerDelegate.AutoReset = true;
            timerDelegate.Enabled = true;

        }


        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            UpdateTieFighters();
            UpdateExplosions();
            CreateNewFighters();
            CheckCrosshairHits();
        }

        /// <summary>
        /// This method moves all existing TieFighters over the screen. It also checks for fighters that have exceeded screen boundaries
        /// </summary>
        private void UpdateTieFighters()
        {
            var toRemove = new List<TieFighter<double>>();
            foreach (TieFighter<double> t in tieFighters)
            {
                t.x += (20 / 1000) * 100 * t.TTP;
                if (t.x > 100)
                    toRemove.Add(t);
            }
            foreach(TieFighter<double> t in toRemove)
            {
                tieFighters.Remove(t);
            }
        }

        /// <summary>
        /// This method reduces the Time To Live of each existing Explosion, removing them when TTL reaches 0;
        /// </summary>
        private void UpdateExplosions()
        {
            var toRemove = new List<Explosion<double>>();
            foreach(Explosion<double> x in explosions)
            {
                x.TTL -= 20;
                if (x.TTL < 0)
                    toRemove.Add(x);
            }
            foreach (Explosion<double> x in toRemove)
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
                tieFighters.Add(new TieFighter<double>(0, 100, 50, 50, 3));
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
