using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class CustomCommands : BaseCommandModule
    {
        [Command("cs")]
        [Description("put C# code in coding channel and display a link to that code.")]
        public async Task CSCode(CommandContext ctx, params string[] msg)
        {
            var codingChannel = ctx.Channel.Guild.GetChannel(648390408568832007);
            string link = "https://discordapp.com/channels/" + ctx.Guild.Id + "/648390408568832007/" + ctx.Message.Id;
            string message = "```cs\n" + ctx.Message.Content.ToString().Remove(0, 3) + "```";

            var embedLink = new DiscordEmbedBuilder()
            {
                Description = $"Code by: [{ctx.Message.Author.Username}]\n{link}",
                Color = ctx.Member.Color,
            };
            var embedCodeTitle = new DiscordEmbedBuilder
            {
                Title = "C# Code: " + ctx.Message.Author.Username,
                Color = ctx.Member.Color,
            };

            await ctx.Channel.SendMessageAsync("", false, embedLink);
            await ctx.Message.DeleteAsync().ConfigureAwait(false);

            await ctx.Client.SendMessageAsync(codingChannel, "", false, embedCodeTitle).ConfigureAwait(false);
            await ctx.Client.SendMessageAsync(codingChannel, message).ConfigureAwait(false);
        }

        [Command("py")]
        [Description("put Python code in coding channel and display a link to that code.")]
        public async Task PYCode(CommandContext ctx, params string[] msg)
        {
            var codingChannel = ctx.Channel.Guild.GetChannel(648390408568832007);
            string link = "https://discordapp.com/channels/" + ctx.Guild.Id + "/648390408568832007/" + ctx.Message.Id;
            string message = "```py\n" + ctx.Message.Content.ToString().Remove(0, 3) + "```";

            var embedLink = new DiscordEmbedBuilder()
            {
                Description = $"Code by: [{ctx.Message.Author.Username}]\n{link}",
                Color = ctx.Member.Color,
            };
            var embedCodeTitle = new DiscordEmbedBuilder
            {
                Title = "Python Code: " + ctx.Message.Author.Username,
                Color = ctx.Member.Color,
            };

            await ctx.Channel.SendMessageAsync("", false, embedLink);
            await ctx.Message.DeleteAsync().ConfigureAwait(false);

            await ctx.Client.SendMessageAsync(codingChannel, "", false, embedCodeTitle).ConfigureAwait(false);
            await ctx.Client.SendMessageAsync(codingChannel, message).ConfigureAwait(false);
        }

    }
}
