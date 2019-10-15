using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tie_Fighter.Others;
namespace Tie_Fighter
{
    
    public partial class FormLogin : Form
    {
        private Others.MediaPlayer mediaPlayer;
        private DirectoryManager directoryManager;
       
        public FormLogin()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            directoryManager = new DirectoryManager();
            mediaPlayer = new Others.MediaPlayer();

            //Launch video
            System.Diagnostics.Process process = System.Diagnostics.Process.Start(directoryManager.IntroVideo);
        }

  
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string name = userNameField.Text;
            string IPAddr = ipAddressField.Text;
            if (AttemptConnect(name, IPAddr))
            {
               // this.Hide();
                FormQueue formQueue = new FormQueue();
                formQueue.Show();
            } 
        }

        public bool AttemptConnect(string name, string IP)
        {
            bool succeed = Connect(name, IP);
            if (!succeed)
            {
                DialogResult result = MessageBox.Show($"Server on IP-address: \"{IP}\" was not found, retry?", "Connection error",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                    succeed = AttemptConnect(name, IP);
            }
            //To continue 
            succeed = true;
            //Remove upper statement later!
            return succeed;
        }

        public bool Connect(string name, string IP)
        {
            return false;
        }
    }
}
