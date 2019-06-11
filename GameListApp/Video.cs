using Newtonsoft.Json;

namespace GameListApp
{
    public class Video
    {
        public int Id { get; set; }
        [JsonProperty("video_id")]
        public string VideoName { get; set; }
    }
}