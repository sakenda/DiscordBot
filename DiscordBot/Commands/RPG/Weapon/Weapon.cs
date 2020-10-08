using Emzi0767.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace DiscordBot.Commands.RPG
{
    internal class Weapon
    {
        #region instance members
        private string weaponName;
        private int attackPower;
        private int blockRate;
        private int counterRate;

        internal string WeaponName { get => weaponName; private set => weaponName = value; }
        internal int AttackPower { get => attackPower; private set => attackPower = value; }
        internal int BlockRate { get => blockRate; private set => blockRate = value; }
        internal int CounterRate { get => counterRate; private set => counterRate = value; }

        internal Weapon(string name)
        {
            weaponName = name;
            attackPower = (int)weapons[name].GetValue(0);
            blockRate = (int)weapons[name].GetValue(1);
            counterRate = (int)weapons[name].GetValue(2);
        }
        #endregion

        #region static members - weapon library
        internal static string Dagger { get => "Dagger"; }
        internal static string Sword { get => "Sword"; }
        internal static string Fist { get => "Fist"; }

        private static readonly Dictionary<string, int[]> weapons = new Dictionary<string, int[]>()
        {
            // int[] order: attack, block, counter
            { nameof(Fist), new int[] { 1, 1, 3 } },
            { nameof(Dagger), new int[] { 2, 2, 2 } },
            { nameof(Sword), new int[] { 3, 3, 1 } }
        };
        #endregion
    }
}
