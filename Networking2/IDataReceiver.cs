using System.Net.Sockets;

namespace Networking
{
    public interface IDataReceiver
    {
        void handlePacket(dynamic data, Client sender);

        void HandleError(Client client);
    }
}