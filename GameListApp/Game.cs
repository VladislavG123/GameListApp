using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameListApp
{
    public class Game
    {
        public int Id { get; set; }
        public int Category { get; set; }
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }
        [JsonProperty("first_release_date")]
        public int FirstReleaseDate { get; set; }
        [JsonProperty("game_modes")]
        public List<int> GameModes { get; set; }
        public List<int> Genres { get; set; }
        [JsonProperty("involved_companies")]
        public List<int> InvolvedCompanies { get; set; }
        public string Name { get; set; }
        public List<int> Platforms { get; set; }
        [JsonProperty("player_perspectives")]
        public List<int> PlayerPerspectives { get; set; }
        public double Popularity { get; set; }
        public List<int> Screenshots { get; set; }
        [JsonProperty("similar_games")]
        public List<int> SimilarGames { get; set; }
        [JsonProperty("updated_at")]
        public int UpdatedAt { get; set; }
        public string Url { get; set; }
        public List<int> Videos { get; set; }
        public List<int> Websites { get; set; }
        public double? Rating { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}