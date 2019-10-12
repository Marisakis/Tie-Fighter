using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Networking;

namespace Tie_Fighter
{
    public partial class FormGame : Form, IDataReceiver
    {
        Client client;

        public FormGame()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            connectToGameServer("Sebas", "localhost");
        }
        
        private void connectToGameServer(string name, string ip)
        {
            client = new Client(new TcpClient(ip, 80), this);
            client.Write("<login>," + name + "<EOF>");
            client.Write("<crosshair>,236,630," + Boolean.TrueString + "<EoF>");

            client.Write("<login>,nooby,<EOF>");
            client.Write("<crosshair>, 236, 630, true,<EoF>");



        }
        public void handlePacket(string[] data, Client sender)
        {
            throw new NotImplementedException();
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

        public Tuple<int,int> getPixelCoordinates(Tuple<double, double> percentageCoordinates)
        {
            int x = (int)Math.Round( this.Size.Width * (percentageCoordinates.Item1 / 100));
            int y = (int)Math.Round(this.Size.Height * (percentageCoordinates.Item2 / 100));
            return new Tuple<int, int>(x,y);

        }

        public Tuple<double, double> getPercentageCoordinates(Tuple<int, int> pixelCoordinates)
        {
            double x = ((double)pixelCoordinates.Item1) / this.Size.Width * 100;
            double y = ((double)pixelCoordinates.Item2) / this.Size.Height * 100;
            return new Tuple<double, double>(x, y);
        }     
    }
}
