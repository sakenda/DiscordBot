using DiscordBot.Attributes;
using DiscordBot.Handler.Dialogue;
using DiscordBot.Handler.Dialogue.Steps;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class ExampleCommands : BaseCommandModule
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

        [Command("dia")]
        public async Task Dialogue(CommandContext ctx)
        {
            var inputStep = new TextStep("Enter 'number' for the next step.", null);
            var intStep = new IntStep("Gimme dat sweet numba....", null, maxValue: 100);

            string input = string.Empty;
            int value = 0;

            inputStep.OnValidResult += (result) =>
            {
                input = result;

                if (result == "number")
                {
                    inputStep.SetNextStep(intStep);
                }
            };

            intStep.OnValidResult += (result) => value = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);
            var inputDialogueHandler = new DialogueHandler(ctx.Client, userChannel, ctx.User, inputStep);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);
            if (!succeeded) return;

            await ctx.Channel.SendMessageAsync(input).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(value.ToString()).ConfigureAwait(false);
        }

        [Command("emojidialogue")]
        public async Task EmojiDialogue(CommandContext ctx)
        {
            var yesStep = new TextStep("You chose Yes", null);
            var noStep = new TextStep("You chose No", null);

            var emojiStep = new ReactionStep("Yes or No?", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                {DiscordEmoji.FromName(ctx.Client, ":thumbsup:"), new ReactionStepData { Content = "Yes", NextStep = yesStep } },
                {DiscordEmoji.FromName(ctx.Client, ":thumbsdown:"), new ReactionStepData { Content = "No", NextStep = noStep } }
            });

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(ctx.Client, userChannel, ctx.User, emojiStep);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);
            if (!succeeded) return;

            //await ctx.Channel.SendMessageAsync(input).ConfigureAwait(false);
        }
    }
}
