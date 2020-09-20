using DiscordBot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("returns pong")]
        [RequireCategories(ChannelCheckMode.Any, "Text Channels")] //Required Channel
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("add")]
        [Description("add two numbers")]
        public async Task Add(CommandContext ctx, [Description("first integer number")] int value1, [Description("second integer number")] int value2)
        {
            await ctx.Channel.SendMessageAsync(

                (value1 + value2).ToString()

                ).ConfigureAwait(false);
        }

        [Command("respond")]
        [Description("respond message back to channel. once per 30 seconds.")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(message.Result.Content).ConfigureAwait(false); ;
        }

        [Command("poll")]
        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojiOptions)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var options = emojiOptions.Select(x => x.ToString());

            var embed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options)
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);

            foreach (var option in emojiOptions)
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);

            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct();
            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }




    }
}
