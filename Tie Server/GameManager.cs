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

        }

        /// <summary>
        /// This method reduces the Time To Live of each existing Explosion, removing them when TTL reaches 0;
        /// </summary>
        private void UpdateExplosions()
        {

        }

        /// <summary>
        /// This method checks if the amount of fighters is beneath a certain trehshold and adds new ones if needed
        /// </summary>
        private void CreateNewFighters()
        {

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
