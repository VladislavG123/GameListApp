using Newtonsoft.Json;

namespace GameListApp
{
    public class Screen
    {
        public int Id { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}