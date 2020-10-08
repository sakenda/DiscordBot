using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Commands.RPG
{
    internal class Enemy : Character
    {
        internal Enemy(string name, int lvl, Weapon weapon) : base(name, lvl, weapon) { }
    }
}
