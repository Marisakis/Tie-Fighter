using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Tie_Fighter.GameObjects;
using Tie_Fighter.GameObjects.Fighters;

namespace Tie_Fighter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormGame formGame = new FormGame();
            List<GameObject<double>> testList = new List<GameObject<double>>();
            TieFighter<double> testFighter = new TieFighter<double>();
            testFighter.x = 200; testFighter.y = 200;
            testList.Add(testFighter);
            formGame.objectManager.gameObjects = testList;
            Application.Run(formGame);
            
        }

        
    }
}
