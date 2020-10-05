using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class UserStashCommands : BaseCommandModule
    {
        private static List<string> users = StashJson.GetUsersAsync().Result;

        [Command("stash")]
        [Description("Stash strings. 10 per User. [.stash add (string)] add a string, [.stash show 0-9] displays a string, [.stash del 0-9] delete a string")]
        public async Task Stash(CommandContext ctx, params string[] str)
        {
            string text = "";

            if (users.Contains(ctx.User.Username))
            {
                if (str[0] == "add")
                {
                    foreach (var item in str.Skip(1))
                        text += item + " ";
                    await ctx.Client.SendMessageAsync(ctx.Channel, text).ConfigureAwait(false);
                }
                else if (str[0] == "del")
                {
                    // Delete content from stash position
                }
                else if (str[0] == "show")
                {
                    // Show content in stash position
                }
            }
            else
            {

            }
        }
    }
}
