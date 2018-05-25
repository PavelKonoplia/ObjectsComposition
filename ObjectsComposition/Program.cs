using ObjectsComposition.Logic;

namespace ObjectsComposition
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string port = "http://localhost:8888/";

            Listener listener = new Listener(port);

            listener.Listen();
        }
    }
}
