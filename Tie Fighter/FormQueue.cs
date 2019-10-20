﻿using Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tie_Fighter.Others;
using Tie_Server;

namespace Tie_Fighter
{
    /// <summary>
    /// The FormQueue is a game lobby.
    /// </summary>
    public partial class FormQueue : Form, IDataReceiver
    {
        private Client client;
        private readonly DirectoryManager directoryManager;
        private readonly Others.MediaPlayer mediaPlayer;
        private Thread updateChatThread;
        private delegate void SafeCallDelegate(string text);
        private delegate void RestartDelegate(Client client, string name);
        private string name;

        /// <summary>
        /// Create a queue and play a in-lobby sound effect.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
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
        /// <summary>
        /// Action on mouse click on start button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartBtn_MouseClick(object sender, MouseEventArgs e)
        {
            FormGame formGame = new FormGame(client, name, this);
            formGame.Show();
            //Starten game GIT code:
            dynamic message = new JObject();
            message.type = "startgame";
            client.Write(message);
            Hide();
        }
        /// <summary>
        /// Restart the lobby.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
        public void Restart(Client client, string name)
        {
            if (InvokeRequired)
            {
                RestartDelegate d = new RestartDelegate(Restart);
                Invoke(d, new object[] { this.client, this.name });
            }
            else
            {
                Focus();
                chatBox.ResetText();
                Show();
                this.name = name;
                this.client.SetDataReceiver(this);
                this.client = client;
            }

        }

        /// <summary>
        /// Send message to server (and all clients) upon "enter" key press.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Handle a packet received from the server.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sender"></param>
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
                    {
                        builder.Append(h.ToString() + "\r\n");
                    }

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
        /// <summary>
        /// Update the chat which invokes on the GUI thread.
        /// </summary>
        /// <param name="chat"></param>
        private void UpdateChat(string chat)
        {
            if (lobbyPlayersLabel.InvokeRequired)
            {
                SafeCallDelegate d = new SafeCallDelegate(UpdateChat);
                lobbyPlayersLabel.Invoke(d, new object[] { chat });
            }
            else
            {
                lobbyPlayersLabel.Text += "\r\n" + chat;
            }
        }

        /// <summary>
        /// Retrieve high score from server when clicking on the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Highscoresbutton_Click(object sender, EventArgs e)
        {
            dynamic message = new JObject();
            message.type = "highscorerequest";
            client.Write(message);
        }
    }
}
