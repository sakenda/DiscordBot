using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class StashJson
    {
        static string path = "UserStash/users.dat";

        public static async Task<List<string>> GetUsersAsync()
        {
            if (!File.Exists(path))
                File.Create(path);

            List<string> users = new List<string>();

            var logFile = ReadLogLines(path);

            foreach (var item in logFile)
                users.Add(item);

            return users;
        }

        private static IEnumerable<string> ReadLogLines(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static async Task SaveNewUserAsync(string username)
        {

        }

    }
}
