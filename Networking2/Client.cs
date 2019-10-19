using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Networking
{
    public class Client
    {
        private TcpClient TcpClient;
        private IDataReceiver dataReceiver { get;  set; }
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        string totalBuffer = String.Empty;

        public Client(TcpClient newTcpClient, IDataReceiver dataReceiver)
        {
            this.TcpClient = newTcpClient;
            this.dataReceiver = dataReceiver;
            Connect();
        }

        public void Connect()
        {
            this.stream = TcpClient.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        public void SetDataReceiver(IDataReceiver dataReceiver)
        {
            this.dataReceiver = dataReceiver;
        }

        public bool GetIsConnected()
        {
            return this.TcpClient.Connected;
        }

        private void OnRead(IAsyncResult ar)
        {
            //Console.WriteLine("Received a message");
            int receivedBytes = stream.EndRead(ar);
            totalBuffer += Encoding.ASCII.GetString(buffer, 0, receivedBytes);
            //Console.WriteLine(totalBuffer);

            while (totalBuffer.Contains("<EOF>"))
            {
                string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("<EOF>"));
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("<EOF>") + 5);
               // Console.WriteLine("End of message found");
                dynamic data = JsonConvert.DeserializeObject(packet);
                dataReceiver.handlePacket(data, this);
            }
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        public void Write(string data)
        {
            try
            {
                data += "<EOF>";
                stream.Write(System.Text.Encoding.ASCII.GetBytes(data), 0, data.Length);
                stream.Flush();
            }
            catch (Exception e)
            {
            }
        }

        public void Write(dynamic message)
        {
            Write(JsonConvert.SerializeObject(message));
        }

        public void Disconnect()
        {
            this.dataReceiver = null;
            this.TcpClient.Close();
        }
    }
}