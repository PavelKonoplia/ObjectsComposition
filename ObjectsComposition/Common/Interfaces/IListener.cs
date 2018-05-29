using System.Net;

namespace ObjectsComposition.Interfaces
{
    public interface IListener
    {
        HttpListener HttpListener { get; }

        void Listen();

        void Stop();
    }
}
