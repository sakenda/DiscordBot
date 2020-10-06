using System;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var bot = new Bot();
                bot.RunAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(new string('=', 100) + "\n" + new string(' ', 100));
                Console.WriteLine(ex.Message + new string(' ', 100 - ex.Message.Length));
                Console.WriteLine(new string(' ', 100) + "\n" + new string('=', 100));
                Console.Beep();

                Console.ResetColor();

                Console.Write("\nClosing Application....");
                Console.ReadKey();
                Environment.Exit(0);
            }



        }
    }
}
