﻿using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using ObjectsComposition.Common;
using ObjectsComposition.Interfaces;
using ObjectsComposition.Logic.DbLogic;
using ObjectsComposition.Models;

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

                try
                {
                    _solver.ObjectFromXml(ConvertStringToXml(input));
                    SendResponse(response, 200);
                }
                catch (ObjectException ex)
                {
                    SendResponse(response, 400);
                    ExceptionProvider.HappenedExceptionRepository.Create(new HappenedException(ex));
                }
                catch (InvalidOperationException ex)
                {
                    SendResponse(response, 400);
                    if (ex.InnerException is ObjectException)
                    {
                        ExceptionProvider.HappenedExceptionRepository.Create(new HappenedException(ex.InnerException as ObjectException));
                    }
                    else
                    {
                        throw ex;
                    }                    
                }                
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SendResponse(HttpListenerResponse response, int statusCode)
        {
            response.StatusCode = statusCode;
            response.Close();
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
