using System.Configuration;
using ObjectsComposition.Logic;

namespace ObjectsComposition
{
    public class Program
    {
        public static void Main()
        {
            string port = "http://localhost:8888/";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Listener listener = new Listener(port, connectionString);

            listener.Listen();
        }
    }
}