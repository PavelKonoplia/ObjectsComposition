using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.SenderForTest
{
    class Sender
    {
        // here is code for main class of test client program

        /*
        static void Main(string[] args)
        {
            User person = new User("Tom", "MeTom", "qwerty", 23);

            string xml = "";

            XmlSerializer formatter = new XmlSerializer(typeof(User));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    formatter.Serialize(writer, person);
                    xml = sww.ToString();
                }
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            SendXML(doc);
        }


        public static string SendXML(XmlDocument xmlFile)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:8888/");
            var bytes = Encoding.ASCII.GetBytes(xmlFile.OuterXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            var requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode.ToString() + " Yes";
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }
        */
    }
}
