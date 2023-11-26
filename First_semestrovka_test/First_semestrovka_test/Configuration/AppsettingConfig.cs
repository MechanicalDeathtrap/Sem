using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oris_First_Semestrovka.Configuration
{
    public class AppsettingConfig
    {
        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Port")]
        public uint Port { get; set; }
        public static string StaticFilePath = @"C:\Users\korik\source\repos\MechanicalDeathtrap\ReposCsNew\First_semestrovka_test\First_semestrovka_test\bin\Debug\net6.0\";//"static";//{ get; set; }

    }
}
