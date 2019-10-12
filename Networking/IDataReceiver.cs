namespace Networking
{
    public interface IDataReceiver
    {
        void handlePacket(string[] data, Client sender);
    }
}