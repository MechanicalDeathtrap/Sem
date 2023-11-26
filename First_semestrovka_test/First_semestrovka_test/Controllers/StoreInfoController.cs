using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using First_semestrovka_test.Attributes;
using Oris_First_Semestrovka.Attributes;
using MyORM;
using Azure.Core;
using Oris_First_Semestrovka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Azure;
using System.Drawing;
using HttpMultipartParser;
using System.Net;

namespace First_semestrovka_test.Controllers
{
    [Controller("StoreInfo")]
    public class StoreInfoController
    {
        [Post("StoreInfoInDataBase")]
        public async void StoreInfoInDataBase(string text, string elementID)
        {
            Console.WriteLine($"text:{text}");
            var getElement = new Database("Data Source=localhost;Initial Catalog=Oris_First_Semestrovka;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                                "Application Intent=ReadWrite;Multi Subnet Failover=False").SelectByElementID<UserText>(elementID);
            /*if ( getElement == null)
            {*/
                new Database("Data Source=localhost;Initial Catalog=Oris_First_Semestrovka;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                                "Application Intent=ReadWrite;Multi Subnet Failover=False")
                    .Insert(new UserText(new Random().Next(0, 50000),text, elementID));

                Console.WriteLine(new Database("Data Source=localhost;Initial Catalog=Oris_First_Semestrovka;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                                "Application Intent=ReadWrite;Multi Subnet Failover=False").SelectByElementID<UserText>(elementID));
            //}
/*            else
            {
                new Database("Data Source=localhost;Initial Catalog=Oris_First_Semestrovka;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                                "Application Intent=ReadWrite;Multi Subnet Failover=False")
                    .Update(new UserText(getElement.ID, text, elementID));
            }*/
            Console.WriteLine(new Database("Data Source=localhost;Initial Catalog=Oris_First_Semestrovka;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                "Application Intent=ReadWrite;Multi Subnet Failover=False").SelectByElementID<UserText>(elementID));
        }
 /*           try
            {
                var request = context.Request;

                var boundary = GetBoundary(request.ContentType);
                var reader = new MultipartReader(boundary, request.InputStream);

                var section = await reader.ReadNextSectionAsync();
                while (section != null)
                {
                    var contentDisposition = section.GetContentDispositionHeader();
                    if (contentDisposition != null)
                    {
                        if (contentDisposition.DispositionType.Equals("form-data", StringComparison.OrdinalIgnoreCase))
                        {
                            var name = contentDisposition.Name;

                            if (section.Body is FileStream fileStream)
                            {
                                // Это файл, обрабатывайте его здесь
                                var fileName = contentDisposition.FileName;
                                // Далее обрабатываете файл по вашим потребностям
                            }
                            else
                            {
                                // Это текстовое поле, обрабатывайте его здесь
                                var fieldValue = await section.ReadAsStringAsync();
                                // Далее обрабатываете текстовое поле по вашим потребностям
                            }
                        }
                    }

                    section = await reader.ReadNextSectionAsync();
                }

                // Отправка ответа клиенту (может быть асинхронной, в зависимости от вашей логики)
                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.OK;
                using (var writer = new StreamWriter(response.OutputStream))
                {
                    writer.Write("Данные успешно получены и обработаны");
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine("Произошла ошибка: " + ex.Message);
                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                using (var writer = new StreamWriter(response.OutputStream))
                {
                    writer.Write("Произошла ошибка при обработке данных");
                }
            }
        }

        private static string GetBoundary(string contentType)
        {
            var elements = contentType.Split(';');
            var element = elements[1].Trim();
            var boundary = element.Substring("boundary=".Length);
            return boundary;
        }*/
    }
}
