using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bot_Launcher.Services.Common;
using Newtonsoft.Json;

namespace Bot_Launcher.Services
{
    public class ConfigService
    {
        public Config CurrentConfiguration { get; private set; } = new Config();
        
        public async Task<Config> LoadConfigAsync(string path = "Resources/config.json")
        {
            string json = "{}";
            var utf8 = new UTF8Encoding(false);
            var fi = new FileInfo(path);
            if (!fi.Exists)
            {
                Console.WriteLine("Loading configuration failed!");

                Directory.CreateDirectory("Resources");

                json = JsonConvert.SerializeObject(new Config(), Formatting.Indented);
                using (FileStream fs = fi.Create())
                using (var sw = new StreamWriter(stream: fs, utf8))
                {
                    await sw.WriteAsync(json);
                    await sw.FlushAsync();
                }

                Console.WriteLine("New default configuration file has been created at:");
                Console.WriteLine("" + fi.FullName);
                Console.WriteLine("Please fill it with appropriate values and re-run the Launcher.");

                throw new IOException("Configuration file not found!");
            }

            using (FileStream fs = fi.OpenRead())
            using (var sr = new StreamReader(fs, utf8))
                json = await sr.ReadToEndAsync();

            this.CurrentConfiguration =
                JsonConvert.DeserializeObject<Config>(json) ?? throw new JsonSerializationException();
            return this.CurrentConfiguration;
        }
        
    }
}