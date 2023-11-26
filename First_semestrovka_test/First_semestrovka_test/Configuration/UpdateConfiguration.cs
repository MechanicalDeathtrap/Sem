using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Oris_First_Semestrovka.Configuration
{
    public class UpdateConfiguration
    {
        private const string configFile = "appsetting.json";
        public static AppsettingConfig config { get; set; }

        public UpdateConfiguration()
        {
            UpdatetConfig();
        }
        public AppsettingConfig UpdatetConfig()
        {
            try
            {
                if (!Directory.Exists(AppsettingConfig.StaticFilePath))
                {
                    Console.WriteLine("Папка static не найдена... Создание новой папки static");
                    var path = AppsettingConfig.StaticFilePath;
                    var direct = Directory.GetCurrentDirectory();
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), AppsettingConfig.StaticFilePath));
                }
                using (FileStream stream = File.OpenRead(configFile))
                {
                    config = JsonSerializer.Deserialize<AppsettingConfig>(stream);
                }
                return config;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"Файл не {configFile} найден");
            }
        }
    }
}
