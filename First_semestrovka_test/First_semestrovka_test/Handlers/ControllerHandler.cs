using Oris_First_Semestrovka.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using System.Xml;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using System.Web;
using HttpMultipartParser;

namespace Oris_First_Semestrovka.Handlers
{
    public class ControllerHandler : Handler
    {
        public override void HandleRequest(HttpListenerContext context)
        {
            try
            {
                var request = context.Request;
                using var response = context.Response;

                var strParams = request.Url
                    .Segments
                    .Skip(1)
                    .Select(s => s.Replace("/", ""))
                    .ToArray();

                if (strParams!.Length < 2)
                    throw new ArgumentNullException("the number of lines in the query string is less than two!");


                using var streamReader = new StreamReader(request.InputStream);
                var tempOfData = streamReader.ReadToEnd();
                string[] formData = new[] { "" };
                if (!String.IsNullOrEmpty(tempOfData))
                {
                    switch (strParams[0])
                    {
                        case "storeInfo":
                            {
                                // вычленить json
                               // var currentOfUserData = tempOfData?.Split('\r');
                               // var image = (currentOfUserData[11]);
                               // formData = new[] { (currentOfUserData[3]), (currentOfUserData[7]), (currentOfUserData[11]) };
                                break;
                            }
                        case "authorize":
                            {
                                var currentOfUserData = tempOfData?.Split('&');
                                formData = new[] { (currentOfUserData[0][9..]), currentOfUserData[1][6..], currentOfUserData[2][9..] };
                                break;
                            }

                    }
                }

                var controllerName = strParams[0];
                var methodName = strParams[1];
                var assembly = Assembly.GetExecutingAssembly();

                var controller = assembly.GetTypes()
                    .Where(t => Attribute.IsDefined(t, typeof(ControllerAttribute)))
                    .FirstOrDefault(c =>
                        ((ControllerAttribute)Attribute.GetCustomAttribute(c, typeof(ControllerAttribute))!)
                        .ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase));

                var method = controller.GetMethods()
                    .FirstOrDefault(t => t.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase));

                var queryParams = new object[] { };

                if (formData.Length > 1)
                {
                    queryParams = method?.GetParameters()
                        .Select((p, i) => Convert.ChangeType(formData[i], p.ParameterType))
                        .ToArray();
                }


                var resultFromMethod = method?.Invoke(Activator.CreateInstance(controller), queryParams);
                Console.WriteLine();
                ProcessResult(resultFromMethod, context);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static async void ProcessResult<T>(T result, HttpListenerContext context)
        {
            switch (result)
            {
                case string resultOfString:
                    {

                        var response = context.Response;
                        response.Redirect( resultOfString);

                        break;
                    }
                case T[] arrayOfObjects:
                    {
                        var response = context.Response;
                        //response.ContentType = DictionaryExtensions._dictOfExtenshions["json"];
                        var json = JsonConvert.SerializeObject(arrayOfObjects, Newtonsoft.Json.Formatting.Indented);
                        var buffer = Encoding.UTF8.GetBytes(json);
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                        break;
                    }
            }
        }
    }
}
