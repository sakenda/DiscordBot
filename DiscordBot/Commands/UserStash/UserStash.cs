﻿using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        internal string GetItem(int position) => stash[position - 1];
        internal string[] GetAllItems() => stash;

        internal void AddItem(int position, string[] item)
        {
            string text = null;
            foreach (var i in item.Skip(2))
                text += i + " ";
            stash[position - 1] = text;

            SaveStash();
        }
        internal void RemoveItem(int position)
        {
            stash[position - 1] = null;
            SaveStash();
        }
        internal void RemoveItem(bool all)
        {
            int counter = 1;
            if (all)
                while (counter < 11)
                {
                    RemoveItem(counter);
                    counter++;
                }
            SaveStash();
        }

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