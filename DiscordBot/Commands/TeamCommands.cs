using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class TeamCommands : BaseCommandModule
    {
        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            var color = new DiscordColor(1, 149, 195);
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Would you like to join?",
                ImageUrl = ctx.Client.CurrentUser.AvatarUrl,
                Color = color,
            };

            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed);

            var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbsDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var reactionResult = await interactivity.WaitForReactionAsync(
                x => x.Message == joinMessage && x.User == ctx.User &&
                (x.Emoji == thumbsUpEmoji || x.Emoji == thumbsDownEmoji));

            if (reactionResult.Result?.Emoji == thumbsUpEmoji)
                await ctx.Channel.SendMessageAsync($"Welcome {ctx.Member.DisplayName}, you joined").ConfigureAwait(false);
            else if (reactionResult.Result?.Emoji == thumbsDownEmoji)
                await ctx.Channel.SendMessageAsync("Ok, maybe next time.").ConfigureAwait(false);

            else
                await ctx.Channel.SendMessageAsync("Abort join...").ConfigureAwait(false);

            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }
    }
}

