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
    public partial class FormServer : Form
    {
        public FormServer()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.ServerIcon;
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program program = new Program(PortNumberField.Text);
        }
    }
}
