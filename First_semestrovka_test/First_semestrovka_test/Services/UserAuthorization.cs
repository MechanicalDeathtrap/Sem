using MyORM;
using First_semestrovka_test.Cookies;
using Oris_First_Semestrovka;
using Oris_First_Semestrovka.Configuration;
using Oris_First_Semestrovka.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace First_semestrovka_test.Services
{
    public class UserAuthorization
    {
        public UserAuthorization() { }
        public bool Authorization(string name, string login, string password)
        {
            try
            {
                Console.WriteLine("Зашли в метод authorization");
                var user = new Database("Data Source=localhost;Initial Catalog=Oris_First_Semestrovka;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                                "Application Intent=ReadWrite;Multi Subnet Failover=False").SelectByLogin<Admin_Table>(login);

                if (user != null)
                {
                    Console.WriteLine("Authorization was done");

                    if (!Cookies.Cookies.AreAuthorizeCookiesExist())
                    {
                        Cookies.Cookies.CreateAuthorizationCookies(user);

                    }
                    return true;
                }

                Console.WriteLine("No such user in db");
                return false;
            }
            catch
            {
                throw new Exception("Cannot authorize user");
            }
        }
    }
}
