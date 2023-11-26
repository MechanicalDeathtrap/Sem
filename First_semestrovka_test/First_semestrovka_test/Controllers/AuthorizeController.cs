using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Azure;
using First_semestrovka_test.Attributes;
using First_semestrovka_test.Services;
using Microsoft.AspNetCore.Http;
using MyORM;
using Oris_First_Semestrovka.Attributes;

namespace Oris_First_Semestrovka.Controllers
{
    [Controller("Authorize")]
    public class AuthorizeController
    {
        [Post("AuthorizeAdmin")]

        public string AuthorizeAdmin( string name, string login, string password)
        {
            Console.WriteLine("Зашли в метод AuthorizeAdmin");
            var isAuthorized = new UserAuthorization().Authorization(name, login, password);

            if (isAuthorized)
            {
                return "/static/index1_admin.html";
            }
            return "Authorization is failed";
        }

        [Get("IsAuthorized")]
        public string IsAuthorized()
        {
            
            return "/static/404.html";
        }
    }
}
