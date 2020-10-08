using DiscordBot.Commands.RPG;
using System;

namespace DiscordBot
{
    public class Test
    {
        public static void TestPlayer()
        {
            Player player = new Player("Sakenda", 1, new Weapon(Weapon.Fist));
            Enemy enemy = new Enemy("Giant", 5, new Weapon(Weapon.Fist));

            Console.WriteLine("  Player " + new string('=', 93) + "\n");

            Console.WriteLine("  Charactername: " + player.CharacterName);
            Console.WriteLine("  Level: " + player.Level);
            Console.WriteLine("  Experience: " + player.CurrentExp + " / " + player.ExpNextLevel);
            Console.WriteLine("  Strength: " + player.CurrentStr);
            Console.WriteLine("  Health: " + player.CurrentHP + " / " + player.CurrentMaxHp);

            Console.WriteLine("  " + new string('-', 100));

            Console.WriteLine("  Weaponname: " + player.CurrentWeapon.WeaponName);
            Console.WriteLine("  Weapon Attackpower: " + player.CurrentWeapon.AttackPower);
            Console.WriteLine("  Blockrate: " + player.CurrentWeapon.BlockRate);
            Console.WriteLine("  Counterrate: " + player.CurrentWeapon.CounterRate);

            Console.WriteLine("\n  Enemy " + new string('=', 94) + "\n");

            Console.WriteLine("  Enemyname: " + enemy.CharacterName);
            Console.WriteLine("  Level: " + enemy.Level);
            Console.WriteLine("  Strength: " + enemy.CurrentStr);
            Console.WriteLine("  Health: " + enemy.CurrentHP + " / " + enemy.CurrentMaxHp);

            Console.WriteLine("  " + new string('-', 100));

            Console.WriteLine("  Weaponname: " + enemy.CurrentWeapon.WeaponName);
            Console.WriteLine("  Weapon Attackpower: " + enemy.CurrentWeapon.AttackPower);
            Console.WriteLine("  Blockrate: " + enemy.CurrentWeapon.BlockRate);
            Console.WriteLine("  Counterrate: " + enemy.CurrentWeapon.CounterRate);

            Console.WriteLine("\n  " + new string('=', 96) + " End");

            Console.WriteLine("  Initilize Fight Scene...");



        }

    }
}
