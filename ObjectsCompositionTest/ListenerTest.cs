using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsComposition.Interfaces;
using ObjectsComposition.Logic;

namespace ObjectsCompositionTest
{
    [TestClass]
    public class ListenerTest
    {
        [TestMethod]
        public void Listen_url_start_IsListening()
        {
            string testUrl = "http://localhost:8888/connection/";
            string connectionString = "Data Source = KONOPLIA; Initial Catalog = ObjectsComposition; Integrated Security = True";

            IListener listener = new Listener(testUrl, connectionString);

            listener.Listen();

            Assert.Equals(listener.HttpListener.IsListening, true);            
        }
    }
}
