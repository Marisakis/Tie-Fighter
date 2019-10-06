using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tie_Fighter
{
    public partial class FormGame : Form
    {
        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: break;
                case Keys.Down: break;
                case Keys.Left: break;
                case Keys.Right: break;
                case Keys.W: break;
                case Keys.S: break;
                case Keys.A: break;
                case Keys.D: break;
            }
        }

        private void FormGame_MouseClick(object sender, MouseEventArgs e)
        {
            int mousePosX = e.X;
            int mousePosY = e.Y;
        }

     
    }
}
