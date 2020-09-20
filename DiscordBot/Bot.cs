using DiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }

        public async Task RunAsync()
        {
            #region load and read from json file
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            #endregion

            #region initialize client and extensions
            var clientConfig = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = DateTime.Now.ToString("dd/MM/yy | HH:mm"),
            };
            Client = new DiscordClient(clientConfig);
            Client.Ready += OnClientReady;

            var commandsconfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = false,
            };
            Commands = Client.UseCommandsNext(commandsconfig);

            Client.UseInteractivity(new InteractivityConfiguration { Timeout = TimeSpan.FromSeconds(60) }); //sets global time for interactivity
            #endregion

            #region register command scripts
            Commands.RegisterCommands<ExampleCommands>();
            Commands.RegisterCommands<CustomCommands>();

            #endregion

            await Client.ConnectAsync().ConfigureAwait(false);
            await Task.Delay(-1).ConfigureAwait(false);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            e.Client.Logger.Log(LogLevel.Information, new EventId(102, "ClientReady"), $"{nameof(DiscordBot)}: Client is ready to process events.");
            return Task.CompletedTask;
        }
    }
}
