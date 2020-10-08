using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class UserStashCommands : BaseCommandModule
    {
        [Command("stash")]
        [Description(
            "10 strings per User.\n\n" +
            "adding a string: ```[.stash add (1-10) (string)]```\n" +
            "displaying a string: ```[.stash show (1-10)]```\n" +
            "deleting a string: ```[.stash del (1-10)] or [.stash delete (1-10)]```\n" +
            "display all strings: ```[.stash showall]```\n" +
            "delete all strings: ```[.stash delall] or [.stash deleteall]```"
            )]
        public async Task Stash(CommandContext ctx, params string[] str)
        {
            if (!StashJson.stashes.ContainsKey(ctx.User.Username))
            {
                // Create new Stash if not created in the past
                StashJson.SaveNewUserAsync(ctx.User.Username);
                await ctx.Client.SendMessageAsync(ctx.Channel, "Stash created").ConfigureAwait(false);
            }
            else
            {
                string function = str[0];
                UserStash stash = StashJson.stashes[ctx.User.Username];
                switch (function)
                {
                    // Add content to Stash
                    case "add":
                        if (int.TryParse(str[1], out int slotNumber) && slotNumber <= 10)
                        {
                            stash.AddItem(slotNumber, str);
                            await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                                ctx,
                                $"{stash.UserName}'s Slot number {slotNumber}",
                                stash.GetItem(slotNumber))
                                ).ConfigureAwait(false);
                        }
                        else
                            await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                                ctx,
                                "Wrong syntax.",
                                "Example: .stash (interaction) [(number)] [(text)]")
                                ).ConfigureAwait(false);
                        break;

                    // Delete content from stash position
                    case "delete":
                    case "del":
                        {
                            if (int.TryParse(str[1], out slotNumber))
                            {
                                stash.RemoveItem(false, slotNumber);
                                await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                                    ctx,
                                    $"{stash.UserName}'s String Stash",
                                    $"String in slot {slotNumber} deleted")
                                    ).ConfigureAwait(false);
                            }
                            else
                                await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                                    ctx,
                                    "Wrong syntax.",
                                    "Example: .stash (interaction) [(number)] [(text)]")
                                    ).ConfigureAwait(false);
                            break;
                        }

                    // Delete all contentslots
                    case "deleteall":
                    case "delall":
                        {
                            stash.RemoveItem(true);
                            await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                                ctx,
                                $"{stash.UserName}'s String Stash",
                                "all strings deleted")
                                ).ConfigureAwait(false);
                            break;
                        }

                    // Show content in stash position
                    case "show":
                        if (int.TryParse(str[1], out slotNumber))
                        {
                            await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                                ctx,
                                $"{stash.UserName}'s Slot number {slotNumber}",
                                stash.GetItem(slotNumber))
                                ).ConfigureAwait(false);
                        }
                        else
                            await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                                ctx,
                                "Wrong syntax.",
                                "Example: .stash (interaction) [(number)] [(text)]")
                                ).ConfigureAwait(false);
                        break;

                    // Show content in stash position
                    case "showall":
                        string[] temp = stash.GetAllItems();
                        await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                            ctx,
                            $"{stash.UserName}'s String Stash",
                            $"1:   {temp[0]}\n" +
                            $"2:   {temp[1]}\n" +
                            $"3:   {temp[2]}\n" +
                            $"4:   {temp[3]}\n" +
                            $"5:   {temp[4]}\n" +
                            $"6:   {temp[5]}\n" +
                            $"7:   {temp[6]}\n" +
                            $"8:   {temp[7]}\n" +
                            $"9:   {temp[8]}\n" +
                            $"10:   {temp[9]}\n"
                            )).ConfigureAwait(false);
                        break;

                    default:
                        await ctx.Client.SendMessageAsync(ctx.Channel, null, false, Embed(
                            ctx,
                            "Wrong syntax.",
                            "Example: .stash (interaction) [(number)] [(text)]")
                            ).ConfigureAwait(false);
                        break;
                }
            }
        }
        private DiscordEmbedBuilder Embed(CommandContext ctx, string title, string content) => new DiscordEmbedBuilder
        {
            Title = title,
            Color = ctx.Member.Color,
            Description = content
        };
    }
}
