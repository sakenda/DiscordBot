using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace DiscordBot
{
    public class UserStash
    {
        internal string UserName => userName;

        [JsonProperty] private string userName;
        [JsonProperty] private string[] stash = new string[10];
        private string path;

        internal UserStash(string user)
        {
            userName = user;
            path = "UserStash/" + user + "Stash.json";
            if (File.Exists(path))
                LoadStash();
        }

        internal void AddItem(int position, string[] item)
        {
            string text = null;
            foreach (var i in item.Skip(1))
                text += item + " ";
            stash[0] = text;

            SaveStash();
        }
        internal void RemoveItem(int position)
        {
            stash[position - 1] = null;
            SaveStash();
        }
        internal string GetItem(int position) => stash[0];

        private void SaveStash()
        {
            var json = JsonConvert.SerializeObject(stash);
            File.WriteAllText(path, json);
        }
        private void LoadStash()
        {
            string json = File.ReadAllText(path);
            stash = JsonConvert.DeserializeObject<string[]>(json);
        }
    }
}
