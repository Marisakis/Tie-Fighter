using Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;
using System.Windows.Forms;
using Tie_Fighter.Others;

namespace Tie_Fighter
{
    /// <summary>
    /// Login window for the client. Can enter server IP-address and port number.
    /// </summary>
    public partial class FormLogin : Form, IDataReceiver
    {
        private readonly Others.MediaPlayer mediaPlayer;
        private readonly DirectoryManager directoryManager;
        private Client client;

        /// <summary>
        /// Constructor to create the login window, and play a video.
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            directoryManager = new DirectoryManager();
            mediaPlayer = new Others.MediaPlayer();

            //Launch video
            System.Diagnostics.Process process = System.Diagnostics.Process.Start(directoryManager.IntroVideo);
        }

        /// <summary>
        /// Action when user clicks on the login button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string name = userNameField.Text;
            string ipAddress = ipAddressField.Text;
            int serverPortNumber = int.Parse(serverPortField.Text);

            if (AttemptConnect(name, ipAddress, serverPortNumber))
            {
                // this.Hide();
                FormQueue formQueue = new FormQueue(client, name);
                Hide();
                formQueue.Show();
            }
        }

        /// <summary>
        /// Attempt to connect to IP and port.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="IP"></param>
        /// <param name="serverPortNumber"></param>
        /// <returns></returns>
        public bool AttemptConnect(string name, string IP, int serverPortNumber)
        {
            bool succeed = Connect(name, IP, serverPortNumber);
            if (!succeed)
            {
                DialogResult result = MessageBox.Show($"Server on IP-address: \"{IP}\" with port: \"{serverPortNumber}\" was not found. Retry?", "IP / port not accepting - error",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    succeed = AttemptConnect(name, IP, serverPortNumber);
                }
            }
            return succeed;
        }

        /// <summary>
        /// Connect with name to ip and port.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ip"></param>
        /// <param name="serverPortNumber"></param>
        /// <returns></returns>
        public bool Connect(string name, string ip, int serverPortNumber)
        {
            try
            {
                client = new Client(new TcpClient(ip, serverPortNumber), this);
                int maxConnectTimeMillis = 1000;
                for (int i = 0; i < maxConnectTimeMillis; i += 100)
                {
                    if (client.GetIsConnected())
                    {
                        dynamic login = new JObject();
                        login.type = "login";
                        login.name = name;
                        client.Write(login);
                        return true;
                    }
                    System.Threading.Thread.Sleep(100);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Handle server data on login.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sender"></param>
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
