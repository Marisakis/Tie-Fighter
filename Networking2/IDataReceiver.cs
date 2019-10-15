namespace Networking
{
    public interface IDataReceiver
    {
        void handlePacket(dynamic data, Client sender);
    }
}