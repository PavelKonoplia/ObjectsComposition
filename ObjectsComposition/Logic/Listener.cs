﻿using System;
using System.IO;
using System.Net;
using ObjectsComposition.Common;
using ObjectsComposition.Interfaces;
using ObjectsComposition.Logic.DbLogic;
using ObjectsComposition.Models;

namespace ObjectsComposition.Logic
{
    public class Listener : IListener
    {
        private ISolver _solver;
        private IRepository<HappenedException> _happenedExceptionRepository;

        public Listener(string url, string connectionString)
        {
            _solver = new Solver(connectionString);
            _happenedExceptionRepository = new CommandRunner<HappenedException>(connectionString);
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
                    _solver.Solve(input);
                    SendResponse(response, 200);
                }
                catch (ObjectException ex)
                {
                    SendResponse(response, 400);
                    Console.WriteLine(ex.Message);
                    _happenedExceptionRepository.Create(new HappenedException(ex));
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.InnerException is ObjectException)
                    {
                        SendResponse(response, 400);
                        Console.WriteLine(ex.InnerException.Message);
                        _happenedExceptionRepository.Create(new HappenedException(ex.InnerException as ObjectException));
                    }
                    else
                    {
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unhandled System Exception:");
                    Console.WriteLine(ex.Message);
                    Stop();
                    break;
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
            Console.WriteLine("Press any key to close app...");
            Console.ReadKey();
        }
    }
}