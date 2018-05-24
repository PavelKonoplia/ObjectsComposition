using System.Net;

namespace ObjectsComposition.Interfaces
{
    public interface IListener
    {
        HttpListener HttpListener { get; set; }

        void Listen(string url);
    }
}
