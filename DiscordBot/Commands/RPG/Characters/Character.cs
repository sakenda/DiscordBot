using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Commands.RPG
{
    internal abstract class Character : IAttackable
    {
        #region Instance Members
        protected string characterName;
        protected int level;
        protected int currentHp;
        protected Weapon currentWeapon;

        internal string CharacterName { get => characterName; private set => characterName = value; }
        internal int Level { get => level; private set => level = value; }

        internal int CurrentStr => strPerLevel[Level];

        internal int CurrentMaxHp => hpPerLevel[Level];
        internal int CurrentHP { get => currentHp; private set => currentHp = value; }

        internal Weapon CurrentWeapon { get => currentWeapon; private set => currentWeapon = value; }

        internal Character(string name, int lvl, Weapon weapon)
        {
            level = lvl;
            characterName = name + " " + new NameSuffix().Suffix;
            currentWeapon = weapon;
        }

        public virtual int Attack() => CurrentStr + CurrentWeapon.AttackPower;
        public virtual int Block(int attack) => attack - CurrentWeapon.BlockRate;
        public virtual int Counter() => CurrentStr * CurrentWeapon.CounterRate;
        #endregion

        #region Static Members
        protected static readonly int[] expPerLevel = { 50, 60, 70, 80, 90, 100, 110, 120, 130, 140 };
        protected static readonly int[] strPerLevel = { 1, 2, 4, 8, 10, 14, 16, 20, 24, 30 };
        protected static readonly int[] hpPerLevel = { 10, 15, 20, 25, 30, 35, 40, 45, 50, 60 };
        protected static readonly int maxLevel = 10;
        #endregion

    }
}
