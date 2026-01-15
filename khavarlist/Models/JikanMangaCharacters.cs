using Newtonsoft.Json;

namespace khavarlist.Models
{
    public class JikanMangaCharacters
    {
        [JsonProperty("data")]
        public required List<JikanMangaCharactersData> Data { get; set; }
    }

    public class JikanMangaCharactersData
    {
        [JsonProperty("character")]
        public required Character character { get; set; }

        [JsonProperty("role")]
        public required string role { get; set; }
    }

    public class Character
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("images")]
        public required Images Images { get; set; }
    }
}
