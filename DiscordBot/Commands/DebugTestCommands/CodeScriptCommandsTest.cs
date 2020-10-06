using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class CodeScriptCommandsTest : BaseCommandModule
    {
        private ConfigJson configJson = ConfigJson.GetJSonAsync().Result;
        private DiscordChannel GetCodeChannel(CommandContext ctx) => ctx.Channel.Guild.GetChannel(configJson.TestChan);

        [Command("xx")]
        [Description("test code.")]
        public async Task XXCode(CommandContext ctx, string headline, params string[] msg)
        {
            await ctx.Client.SendMessageAsync(GetCodeChannel(ctx), "", false, Embed(ctx, "C#")).ConfigureAwait(false);
            var newMessage =
                await ctx.Client.SendMessageAsync(GetCodeChannel(ctx), BuildMessage(ctx, configJson.CsPrefix, headline)).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync("", false, Embed(ctx, newMessage, headline)).ConfigureAwait(false);
            await ctx.Message.DeleteAsync().ConfigureAwait(false);
        }

        private DiscordEmbedBuilder Embed(CommandContext ctx, string codeLanguage)
        {
            return new DiscordEmbedBuilder
            {
                Title = $"{codeLanguage} Code: " + ctx.Message.Author.Username,
                Color = ctx.Member.Color,
            };
        }
        private DiscordEmbedBuilder Embed(CommandContext ctx, DiscordMessage message, string headline)
        {
            if (headline.StartsWith("[") && headline.EndsWith("]"))
            {
                return new DiscordEmbedBuilder
                {
                    Description = $"[{headline}]({message.JumpLink.AbsoluteUri})",
                    Color = ctx.Member.Color,
                };
            }
            return new DiscordEmbedBuilder
            {
                Description = $"[LINK]({message.JumpLink.AbsoluteUri})",
                Color = ctx.Member.Color,
            };
        }
        private string BuildMessage(CommandContext ctx, string prefix, string headline)
        {
            int removeChar = prefix.Length - 3;

            if (headline.StartsWith("[") && headline.EndsWith("]"))
                removeChar += headline.Length + 2;

            return
                prefix +
                ctx.Message.Content.ToString().Remove(0, removeChar) +
                configJson.CodeSuffix;
        }

    }
}
