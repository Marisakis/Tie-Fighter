using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tie_Server
{
    /// <summary>
    /// Fancy startup window to select a specific server port and start hosting the server.
    /// </summary>
    public partial class FormServer : Form
    {
        /// <summary>
        /// Initialise Form items.
        /// </summary>
        public FormServer()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.ServerIcon;
        }
        /// <summary>
        /// Upon startbutton retrieve port number, launch the server and hide the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program program = new Program(PortNumberField.Text);
        }
    }
}
