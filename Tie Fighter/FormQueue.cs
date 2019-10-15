using Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tie_Fighter.Others;

namespace Tie_Fighter
{
    public partial class FormQueue : Form
    {
        private Client client;
        private DirectoryManager directoryManager;
        private Others.MediaPlayer mediaPlayer;

        public FormQueue(Client client)
        {
            InitializeComponent();
            this.client = client;
            directoryManager = new DirectoryManager();
            mediaPlayer = new Others.MediaPlayer();
            mediaPlayer.PlayFile(directoryManager.Good, null);
        }

        private void StartBtn_MouseClick(object sender, MouseEventArgs e)
        {
            FormGame formGame = new FormGame(this.client);
            formGame.Show();
        }
    }
}
