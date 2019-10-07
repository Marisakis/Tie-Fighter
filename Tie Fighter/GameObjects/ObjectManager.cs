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
        private Form form;

        public ObjectManager(Form form)
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
            this.gameObjects = new List<GameObject<int>>();
            
        }

        internal void onResize(FormGame formGame, EventArgs e)
        {
            //Not implemented
        }
    }
}
