using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;

namespace DiscordBot
{
    public class UserStash
    {
        [JsonProperty] private string userName;
        [JsonProperty] private string[] stash = new string[10];
        private string path;

        public UserStash(string user)
        {
            userName = user;
            path = "//UserStash//" + user + "Stash.json";
        }

        public void AddItem(int position, string[] item)
        {
            string text = null;
            foreach (var i in item.Skip(1))
                text += item + " ";
            stash[position - 1] = text;

            SaveStash(this);
        }
        public void RemoveItem(int position) => stash[position - 1] = null;
        public string GetItem(int position) => stash[position - 1];

        private static void SaveStash(UserStash stash)
        {
            var json = JsonConvert.SerializeObject(stash);
            File.WriteAllText(stash.path, json);
        }
    }
}
