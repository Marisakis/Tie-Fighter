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

            //Code to test addition and displaying of a Tie Fighter with Object Manager
            FormGame formGame = new FormGame();
            List<GameObject<int>> testList = new List<GameObject<int>>();
            TieFighter<int> testFighter = new TieFighter<int>(50,50,1,1,3);
            //testFighter.x = 200; testFighter.y = 200;
            testList.Add(testFighter);
            ObjectManager manager = new ObjectManager(formGame);
            manager.updateObjects(testList);
            formGame.objectManager = manager;

            Tuple<double, double> coord1 = new Tuple<double, double>(1, 1);
            Tuple<double, double> coord2 = new Tuple<double, double>(25, 75);
            Tuple<double, double>[] list = new Tuple<double, double>[2];
            list[1] = coord1; list[0] = coord2;
            manager.receiveNewTieFighterList(list);

            Application.Run(formGame);

            
            //Application.Run(new FormGame);

        }


    }
}
