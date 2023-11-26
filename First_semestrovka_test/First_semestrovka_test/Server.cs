using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Oris_First_Semestrovka.Configuration;
using Oris_First_Semestrovka.Handlers;

namespace Oris_First_Semestrovka
{
    public class Server
    {
        public HttpListener listener {  get; set; }
        private AppsettingConfig appsettingConfig = (new UpdateConfiguration()).UpdatetConfig();
        private StaticFileHandlers staticFileHandler = new StaticFileHandlers();
        private ControllerHandler controllerHandler = new ControllerHandler();
        public Server() 
        {
            listener = new HttpListener();
            listener.Prefixes.Add($"{appsettingConfig.Address}:{appsettingConfig.Port}/");
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Сервер запущен");

            listener.Start();

            try
            {
                while (true)
                {
                    var context = listener.GetContext();
                    staticFileHandler.Successor = controllerHandler;
                    staticFileHandler.HandleRequest(context);
                }
            }
            catch
            {
                throw;
            }
            finally 
            {
                listener.Stop();
                Console.WriteLine("Сервер остановлен");
            }
        }
    }
}
