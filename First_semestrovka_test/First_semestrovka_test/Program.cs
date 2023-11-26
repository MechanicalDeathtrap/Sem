using Oris_First_Semestrovka;
using System.Net.Security;

class Program
{
    static async Task Main(string[] args)
    {
        var server = new Server();
        await server.StartAsync();
    }
}