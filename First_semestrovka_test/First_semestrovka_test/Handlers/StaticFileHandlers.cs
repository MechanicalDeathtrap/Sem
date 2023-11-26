using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyORM;
using Oris_First_Semestrovka.Configuration;

namespace Oris_First_Semestrovka.Handlers
{
    public class StaticFileHandlers : Handler
    {
        private AppsettingConfig appsettingConfig = UpdateConfiguration.config;

        public override async void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var absoluteUrl = request.Url.AbsolutePath;
            var staticFilePath = Path.Combine(AppsettingConfig.StaticFilePath, absoluteUrl.Trim('/'));

            try
            {
                if (absoluteUrl.Split('/').Last().Contains('.'))
                {
                    var fileExtension = absoluteUrl.Split('/').LastOrDefault();
                    fileExtension = '.' + fileExtension.Split('.').Last();

                    if (File.Exists(staticFilePath.Replace("\\", "/")))
                    {
                        var type = GetContentType(fileExtension);
                        response.ContentType = type;
                        byte[] bytes;

/*                        if (type.Split('/')[0] == "image")
                        {
                            var imageName = absoluteUrl.Split('/')[3];
                            bytes = GetImageDataFromDatabase(imageName);*/

                            //Console.WriteLine(bytes.Length);
                           // Console.WriteLine(File.ReadAllBytes(staticFilePath).Length);

                           // response.ContentLength64 = bytes.Length;
                        //}

/*                        else
                        {*/
                        bytes = File.ReadAllBytes(staticFilePath);
                        response.ContentLength64 = bytes.Length;


                            //response.ContentLength64 = bytes.Length;

                        using Stream output = response.OutputStream;

                        await output.WriteAsync(bytes);
                        await output.FlushAsync();
                       // }
                    }
                    else
                    {

                        byte[] siteBytes = File.ReadAllBytes(@"C:\Users\korik\source\repos\MechanicalDeathtrap\ReposCsNew\Oris_First_Semestrovka\Oris_First_Semestrovka\bin\Debug\net6.0\static\404.html");

                        response.ContentLength64 = siteBytes.Length;
                        response.StatusCode = 404;
                        using Stream output = response.OutputStream;
                        await output.WriteAsync(siteBytes);
                        await output.FlushAsync();
                    }
                }
                else if (Successor != null)
                {
                    Successor.HandleRequest(context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine();
                throw;
            }
        }

/*        public byte[] GetImageDataFromDatabase(string imageName)
        {
            return new Database("Data Source=localhost;Initial Catalog=Oris_First_Semestrovka;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                "Application Intent=ReadWrite;Multi Subnet Failover=False").SelectByName<OrisImages>(imageName);
            
        }*/

        private string GetContentType(string fileExtension)
        {
            Dictionary<string, string> contentTypeMappings = new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".webp", "image/webp" },
                { ".svg", "image/svg+xml" },
                { ".html", "text/html" }, 
                { ".css", "text/css" },   
                { ".js", "application/javascript" }
            };

            if (contentTypeMappings.ContainsKey(fileExtension))
            {
                return contentTypeMappings[fileExtension];
            }

            return "application/octet-stream"; 
        }
    }
}
