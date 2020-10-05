using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class ConfigJson
    {
        [JsonProperty("token")] internal string Token { get; private set; }
        [JsonProperty("prefix")] internal string Prefix { get; private set; }
        [JsonProperty("codingChannel")] internal ulong CodeChan { get; private set; }
        [JsonProperty("testingChannel")] internal ulong TestChan { get; private set; }
        [JsonProperty("cs")] internal string CsPrefix { get; private set; }
        [JsonProperty("py")] internal string PyPrefix { get; private set; }
        [JsonProperty("codeSuffix")] internal string CodeSuffix { get; private set; }

        public static async Task<ConfigJson> GetJSonAsync()
        {
            string configFilePath = "config1.json";

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine("No Config found, please enter BotToken from 'https://discord.com/developers/applications/': ");

                // need exception handler
                var configContent = CreateConfig(Console.ReadLine());

                var serCont = JsonConvert.SerializeObject(configContent);
                File.WriteAllText(configFilePath, serCont);
                Console.WriteLine($"Configfile created. Starting Bot....");
            }

            var json = string.Empty;
            using (var fs = File.OpenRead(configFilePath))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ConfigJson>(json);
        }

        private static object CreateConfig(string tokenID) => new
        {
            token = tokenID,
            codingChannel = 648390408568832007,
            testingChannel = 756866685054746625,
            prefix = ".",
            cs = "```cs\n",
            py = "```py\n",
            codeSuffix = "```",
        };
    }
}