using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class UserStashCommands : BaseCommandModule
    {
        [Command("stash")]
        [Description("Stash strings. 10 per User. [.stash add (string)] add a string, [.stash show 0-9] displays a string, [.stash del 0-9] delete a string")]
        public async Task Stash(CommandContext ctx, params string[] str)
        {
            if (StashJson.stashes.ContainsKey(ctx.User.Username))
            {
                UserStash stash = StashJson.stashes[ctx.User.Username];
                if (str[0] == "add")
                {
                    stash.AddItem(1, str);
                    await ctx.Client.SendMessageAsync(ctx.Channel, "string saved").ConfigureAwait(false);
                }
                else if (str[0] == "del")
                {
                    // Delete content from stash position
                }
                else if (str[0] == "show")
                {
                    // Show content in stash position
                    await ctx.Client.SendMessageAsync(ctx.Channel, stash.GetItem(1)).ConfigureAwait(false);
                }
            }
            else
            {
                StashJson.SaveNewUserAsync(ctx.User.Username);
                await ctx.Client.SendMessageAsync(ctx.Channel, "Stash created").ConfigureAwait(false);
            }
        }
    }
}
