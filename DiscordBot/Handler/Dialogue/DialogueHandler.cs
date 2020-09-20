﻿using DiscordBot.Handler.Dialogue.Steps;
using DSharpPlus;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot.Handler.Dialogue
{
    class DialogueHandler
    {
        private readonly DiscordClient _client;
        private readonly DiscordChannel _channel;
        private readonly DiscordUser _user;
        private IDialogueStep _currentStep;

        public DialogueHandler(DiscordClient client, DiscordChannel channel, DiscordUser user, IDialogueStep startingStep)
        {
            _client = client;
            _channel = channel;
            _user = user;
            _currentStep = startingStep;
        }

        private readonly List<DiscordMessage> messages = new List<DiscordMessage>();

        public async Task<bool> ProcessDialogue()
        {
            while (_currentStep != null)
            {
                _currentStep.OnMessageAdded += (message) => messages.Add(message);

                bool cancelled = await _currentStep.ProcessStep(_client, _channel, _user);

                if (cancelled)
                {
                    await DeleteMesseges().ConfigureAwait(false);
                    var cancelEmbed = new DiscordEmbedBuilder
                    {
                        Title = "The dialogue has successfully been cancelled",
                        Description = _user.Mention,
                        Color = DiscordColor.Green,
                    };

                    await _channel.SendMessageAsync(embed: cancelEmbed).ConfigureAwait(false);
                    return false;
                }

                _currentStep = _currentStep.NextStep;
            }

            await DeleteMesseges().ConfigureAwait(false);
            return true;
        }

        private async Task DeleteMesseges()
        {
            if (_channel.IsPrivate)
                return;
            foreach (var message in messages)
                await message.DeleteAsync().ConfigureAwait(false);
        }
    }
}
