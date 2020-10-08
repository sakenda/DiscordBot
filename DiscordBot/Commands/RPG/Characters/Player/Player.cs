using System;

namespace DiscordBot.Commands.RPG
{
    internal class Player : Character
    {
        private int currentExp;

        internal int ExpNextLevel => expPerLevel[Level];
        internal int CurrentExp { get => currentExp; private set => currentExp += value; }

        internal Player(string name, int lvl, Weapon weapon) : base(name, lvl, weapon) { }
    }
}
