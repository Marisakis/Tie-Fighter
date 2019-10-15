using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Networking
{
    public class Client
    {
        private TcpClient newTcpClient;
        private IDataReceiver dataReceiver;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        string totalBuffer = "";

        public Client(TcpClient newTcpClient, IDataReceiver dataReceiver)
        {
            this.newTcpClient = newTcpClient;
            this.dataReceiver = dataReceiver;
            this.stream = newTcpClient.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
            
        }

        private void OnRead(IAsyncResult ar)
        {
            Console.WriteLine("Received a message");
            int receivedBytes = stream.EndRead(ar);
            totalBuffer += System.Text.Encoding.ASCII.GetString(buffer, 0, receivedBytes);

            while (totalBuffer.Contains("<EOF>"))
            {
                string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("<EOF>"));
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("<EOF>") + 5);

                string[] data = Regex.Split(packet, ",");
                dataReceiver.handlePacket(data, this);
            }

            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);

        }

        public void Write(string data)
        {
            Console.WriteLine("Sending message: " + data);
            stream.Write(System.Text.Encoding.ASCII.GetBytes(data), 0, data.Length);
            stream.Flush();
        }
    }
}