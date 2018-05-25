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

            IListener listener = new Listener(testUrl);

            listener.Listen();

            Assert.Equals(listener.HttpListener.IsListening, true);            
        }
    }
}
