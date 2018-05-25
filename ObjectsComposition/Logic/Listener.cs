using System;
using System.IO;
using System.Net;
using System.Xml;
using ObjectsComposition.Common;
using ObjectsComposition.Interfaces;

namespace ObjectsComposition.Logic
{
    public class Listener : IListener
    {
        private ISolver _solver;

        public Listener(string url)
        {
            _solver = new Solver();
            HttpListener = new HttpListener();
            HttpListener.Prefixes.Add(url);
        }

        public HttpListener HttpListener { get; }

        public void Listen()
        {
            try
            {
                HttpListener.Start();
                Console.WriteLine("Listening started");

                while (true)
                {
                    HttpListenerContext context = HttpListener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    string input;
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        input = reader.ReadToEnd();
                    }

                    _solver.ObjectFromXml(ConvertStringToXml(input));
                    
                    // TODO response
                    /*
                    string responseString = "502";

                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    output.Close();*/
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Stop()
        {
            HttpListener.Stop();
            Console.WriteLine("Listening stoped");
            Console.Read();
        }

        private XmlDocument ConvertStringToXml(string stringedXml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(stringedXml);
                if (doc == null)
                {
                    throw new IncorectFormatException();
                }

                return doc;
            }
            catch (Exception)
            {
                throw new IncorectFormatException();
            }
        }
    }
}
