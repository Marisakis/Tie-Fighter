using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tie_Fighter.GameObjects.Crosshairs;
using Tie_Fighter.GameObjects.Fighters;

namespace Tie_Fighter.GameObjects
{
    /// <summary>
    /// Local clientside Object Manager. This class keeps track of all objects and allows them to be drawn
    /// </summary>
    public class ObjectManager
    {
        private List<GameObject<int>> gameObjects { get; set; }
        private Crosshair<double> crosshair { get; set; }
        private FormGame form;

        public ObjectManager(FormGame form)
        {
            this.gameObjects = new List<GameObject<int>>();
            this.form = form;
        }

        internal void PaintObjects(Graphics graphics)
        {
            foreach (GameObject<int> g in gameObjects)
            {
                g.Draw(graphics);
            }
        }

        /// <summary>
        /// Receives a list with coordinates in percentages
        /// </summary>
        /// <param name="newList"></param>
        public void updateObjects(List<GameObject<int>> newList)
        {
            this.gameObjects = newList;
        }

        /// <summary>
        /// Takes list of tuples, creates Tie Fighters to display on screen
        /// </summary>
        /// <param name="list"></param>
        public void receiveNewTieFighterList(Tuple<double, double>[] list)
        {
            System.Diagnostics.Debug.WriteLine("Receiving new Tie Fighter List, converting to pixels");
            this.gameObjects = new List<GameObject<int>>();
            foreach (Tuple<double, double> tuple in list)
            {
                Tuple<int, int> coordinates = form.getPixelCoordinates(tuple);
                Tuple<int, int> scaling = new Tuple<int, int>(form.Size.Width / 20, form.Size.Height / 20);
                TieFighter<int> newFighter = new TieFighter<int>(coordinates.Item1, coordinates.Item2, scaling.Item1, scaling.Item2, 3);
                System.Diagnostics.Debug.WriteLine("New TieFighter at coordinates: " + newFighter.x + ", " + newFighter.y);
                this.gameObjects.Add(newFighter);
            }

              
        }

        internal void onResize(FormGame formGame, EventArgs e)
        {
            //Not implemented
        }
    }
}
