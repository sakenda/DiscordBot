using DiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Bot
    {
        internal static DiscordClient Client { get; private set; }
        internal CommandsNextExtension Commands { get; private set; }
        internal InteractivityExtension Interactivity { get; private set; }

        internal ConfigJson configJson = ConfigJson.GetJSonAsync().Result;
        internal StashJson stashJson = new StashJson();

        public async Task RunAsync()
        {
            #region Client setup
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
            Client.UseInteractivity(new InteractivityConfiguration { Timeout = TimeSpan.FromSeconds(60) }); //sets global time for interactivity
            #endregion

            #region Commands setup
            var commandsconfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = false,
            };
            Commands = Client.UseCommandsNext(commandsconfig);
            Commands.RegisterCommands<CodeScriptCommandsTest>();
            Commands.RegisterCommands<ExampleCommands>();
            Commands.RegisterCommands<CodeScriptCommands>();
            Commands.RegisterCommands<UserStashCommands>();
            #endregion

            stashJson.GetUsersStash();

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
