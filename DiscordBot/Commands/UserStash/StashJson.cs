using System;
using System.Collections.Generic;
using System.IO;

namespace DiscordBot
{
    public class StashJson
    {
        internal static Dictionary<string, UserStash> stashes = new Dictionary<string, UserStash>();

        private static string path = "UserStash/users.dat";

        internal Dictionary<string, UserStash> GetUsersStash()
        {
            if (!File.Exists(path)) File.Create(path);

            var logFile = ReadLogLines(path);
            foreach (var item in logFile)
            {
                stashes.Add(item, new UserStash(item));
                Console.WriteLine("- " + item);
            }

            return stashes;
        }

        private static IEnumerable<string> ReadLogLines(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                    yield return line;
            }
        }

        public static void SaveNewUserAsync(string username)
        {
            if (!stashes.ContainsKey(username))
            {
                stashes.Add(username, new UserStash(username));
                using StreamWriter sw = File.AppendText(path);
                sw.WriteLine(username);
            }
            else
                Console.WriteLine("User already have a stash.");

        }

    }
}
