using Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Tie_Fighter.Others;
using Tie_Server;

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
            string ipAddress = ipAddressField.Text;
            int serverPortNumber = Int32.Parse(serverPortField.Text);

            if (AttemptConnect(name, ipAddress, serverPortNumber))
            {
                // this.Hide();
                FormQueue formQueue = new FormQueue(this.client);
                formQueue.Show();
            }
        }

        public bool AttemptConnect(string name, string IP, int serverPortNumber)
        {
            bool succeed = Connect(name, IP, serverPortNumber);
            if (!succeed)
            {
                DialogResult result = MessageBox.Show($"Server on IP-address: \"{IP}\" with port: \"{serverPortNumber}\" was not found. Retry?", "IP / port not accepting - error",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                    succeed = AttemptConnect(name, IP, serverPortNumber);
            }
            return succeed;
        }

        public bool Connect(string name, string ip, int serverPortNumber)
        {
            try
            {
                this.client = new Client(new TcpClient(ip, serverPortNumber), this);
                int maxConnectTimeMillis = 1000;
                for (int i = 0; i < maxConnectTimeMillis; i++)
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
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void handlePacket(dynamic data, Client sender)
        {
            //handle login response here
            switch ((string)data.type)
            {
                case "loginAccept":
                    break;
                default:
                    break;

            }


        }
    }
}
