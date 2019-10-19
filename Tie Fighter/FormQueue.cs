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
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Tie_Server;
using Newtonsoft.Json;
using System.Threading;

namespace Tie_Fighter
{
    public partial class FormQueue : Form, IDataReceiver
    {
        private Client client;
        private DirectoryManager directoryManager;
        private Others.MediaPlayer mediaPlayer;
        private Thread updateChatThread;
        private delegate void SafeCallDelegate(string text);
        private delegate void RestartDelegate(Client client, string name);
        private string name;

        public FormQueue(Client client, string name)
        {
            this.name = name;
            InitializeComponent();
            this.client = client;
            this.client.SetDataReceiver(this);
            directoryManager = new DirectoryManager();
            mediaPlayer = new Others.MediaPlayer();
            mediaPlayer.PlayFile(directoryManager.Good, null);
        }

        private void StartBtn_MouseClick(object sender, MouseEventArgs e)
        {
            FormGame formGame = new FormGame(this.client, this.name, this);
            formGame.Show();
            //Starten game GIT code:
            dynamic message = new JObject();
            message.type = "startgame";
            client.Write(message);
            this.Hide();
        }

        public void Restart(Client client, string name)
        {
            if (this.InvokeRequired)
            {
                var d = new RestartDelegate(Restart);
                this.Invoke(d, new object[] { this.client, this.name });

            }
            else
            {
                Console.WriteLine("attempting to restart lobby");

                this.Show();
                this.name = name;
                this.client = client;
                this.client.SetDataReceiver(this);
                Console.WriteLine("showing lobby");

            }

        }

        private void Highscoresbutton_Click(object sender, EventArgs e)
        {
            dynamic message = new JObject();
            message.type = "highscorerequest";
            client.Write(message);
        }

        private void ChatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dynamic message = new JObject();
                message.type = "chatmessage";
                message.data = $"{name}: {chatBox.Text}";
                client.Write(message);
                chatBox.Text = "";
            }
        }

        public void handlePacket(dynamic data, Client sender)
        {
            switch ((string)data.type)
            {
                case "highscores":
                    StringBuilder builder = new StringBuilder();
                    List<HighScore> highscores = new List<HighScore>();
                    if (data.data != null)
                    {
                        dynamic scores = data.data;
                        for (int i = 0; i < scores.Count; i++)
                        {
                            dynamic value = scores[i];
                            value = value.Value;
                            HighScore highScore = JsonConvert.DeserializeObject<HighScore>(value.ToString());
                            highscores.Add(highScore);

                        }
                    }
                    foreach (HighScore h in highscores)
                        builder.Append(h.ToString() + "\r\n");
                    MessageBox.Show(builder.ToString(), "highscores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "chatmessage":
                    string message = data.data;
                    updateChatThread = new Thread(() => UpdateChat(message));
                    updateChatThread.Start();
                    break;
                default:
                    break;

            }

        }
        private void UpdateChat(string chat)
        {
            if (lobbyPlayersLabel.InvokeRequired)
            {
                var d = new SafeCallDelegate(UpdateChat);
                lobbyPlayersLabel.Invoke(d, new object[] { chat });
            }
            else
            {
                lobbyPlayersLabel.Text += "\r\n" + chat;
            }
        }
    }
}
