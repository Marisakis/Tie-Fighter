using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Tie_Server
{
    internal class Client
    {
        private TcpClient newTcpClient;
        private Program program;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        string totalBuffer = "";

        public Client(TcpClient newTcpClient, Program program)
        {
            this.newTcpClient = newTcpClient;
            this.program = program;
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

                string[] data = Regex.Split(packet, "<EOF>");
                handlePacket(data);
            }

            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);

        }

        private void handlePacket(string[] data)
        {
            throw new NotImplementedException();
        }

        private void Write(string data)
        {
            Console.WriteLine("Sending message: " + data);
            stream.Write(System.Text.Encoding.ASCII.GetBytes(data), 0, data.Length);
            stream.Flush();
        }
    }
}