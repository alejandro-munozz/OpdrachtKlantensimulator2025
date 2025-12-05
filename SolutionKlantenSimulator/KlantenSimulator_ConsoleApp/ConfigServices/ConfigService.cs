using KlantenSimulator_ConsoleApp.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace KlantenSimulator_ConsoleApp.ConfigServices
{
    public class ConfigService
    {
        private readonly string pad;

        public ConfigService(string cfgPad)
        {
            pad = cfgPad;
        }

        public BestandsLezerConfig LoadConfig()
        {
            string json = File.ReadAllText(pad);

            var config = JsonSerializer.Deserialize<BestandsLezerConfig>(json);

            return config;
        }
    }
}
