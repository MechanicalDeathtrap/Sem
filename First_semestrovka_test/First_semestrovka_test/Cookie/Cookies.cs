using MyORM;
using Oris_First_Semestrovka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace First_semestrovka_test.Cookies
{
    public class Cookies
    {
        public static CookieContainer Container =  new CookieContainer();
        private AppsettingConfig AppsettingConfig = (new UpdateConfiguration()).UpdatetConfig();

        public static async void CreateAuthorizationCookies(Admin_Table admin)
        {
            try
            {
                Console.WriteLine(Container.Count);
               // var uri = new Uri($"http//{AppsettingConfig.Address}:{AppsettingConfig.Port}/authorize/authorizeAdmin");
                var uri = new Uri("http://127.0.0.1:2323/static/index1.html");

                var nameCookie = new Cookie("Name", admin.admin_name);
                var loginCookie = new Cookie("Login", admin.admin_login);
                var passwordCookie = new Cookie("Password", admin.admin_password);

                Container.Add(uri, nameCookie);
                Container.Add(uri, loginCookie);
                Container.Add(uri, passwordCookie);


                Console.WriteLine("Cookies has been added!");

/*                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("cookie", Container.GetCookieHeader(uri));

                using var response = await client.GetAsync(uri);
                string responseText = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseText);*/
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("Failed to add cookies");
            }
        }

        public static bool AreAuthorizeCookiesExist()
        {
            return Container.Count != 0;
        }
    }
}
