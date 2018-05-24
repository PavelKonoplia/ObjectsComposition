using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsComposition.Interfaces;

namespace ObjectsCompositionTest
{
    [TestClass]
    public class ListenerTest
    {
        [TestMethod]
        public void Listen_url_start_IsListening()
        {
            IListener listener;

            string testUrl = "http://localhost:8888/connection/";
            listener.Listen(testUrl);

            Assert.Equals(listener.HttpListener.IsListening,true);            
        }
    }
}
