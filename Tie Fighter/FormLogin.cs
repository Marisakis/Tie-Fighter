using Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tie_Fighter.Others;
namespace Tie_Fighter
{

    public partial class FormLogin : Form, IDataReceiver
    {
        private Others.MediaPlayer mediaPlayer;
        private DirectoryManager directoryManager;
        private Client client;

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
                FormQueue formQueue = new FormQueue(this.client);
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
            return succeed;
        }

        public bool Connect(string name, string IP)
        {
            this.client = new Client(new TcpClient(IP, 1717), this);
            for (int i = 0; i < 1001; i++)
            {
                if (client.GetIsConnected())
                {
                    dynamic login = new JObject();
                    login.type = "login";
                    login.name = name;
                    client.Write(login);
                    return true;
                }
                System.Threading.Thread.Sleep(1);
            }
            return false;
        }

        public void handlePacket(dynamic data, Client sender)
        {
            //handle login response here
        }
    }
}
