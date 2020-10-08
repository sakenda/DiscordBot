using System;

namespace DiscordBot.Commands.RPG
{
    internal class NameSuffix
    {
        private string[] suffix = new string[10]
        {
            "the Slayer",
            "the Bearded",
            "the Tiny",
            "the Hammer",
            "the Lucky One",
            "the Loney Pea",
            "the Grinder",
            "the Butcher",
            "the Grumpy",
            "the Almighty",
        };
        internal string Suffix
        {
            get
            {
                var rand = new Random();
                return suffix[rand.Next(1, 10)];
            }
        }
    }
}
