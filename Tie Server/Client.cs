using System.Net.Sockets;

namespace Tie_Server
{
    internal class Client
    {
        private TcpClient newTcpClient;
        private Program program;

        public Client(TcpClient newTcpClient, Program program)
        {
            this.newTcpClient = newTcpClient;
            this.program = program;
        }
    }
}