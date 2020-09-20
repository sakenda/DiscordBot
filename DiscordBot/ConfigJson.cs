using DSharpPlus.Entities;
using Newtonsoft.Json;

namespace DiscordBot
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
        //[JsonProperty("color1")]
        //public DiscordColor Color1 { get; private set; }
        //[JsonProperty("color2")]
        //public DiscordColor Color2 { get; private set; }
        //[JsonProperty("color3")]
        //public DiscordColor Color3 { get; private set; }
        //[JsonProperty("color4")]
        //public DiscordColor Color4 { get; private set; }
        //[JsonProperty("color5")]
        //public DiscordColor Color5 { get; private set; }
    }
}
