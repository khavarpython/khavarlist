using khavarlist.Controllers;
using Newtonsoft.Json;

namespace khavarlist.Models
{
    public class JikanMangaList
    {
        [JsonProperty("data")]
        public required List<JikanMangaData> Data { get; set; }

        [JsonProperty("pagination")]
        public required Pagination Pagination { get; set; }
    }

    public class JikanMangaSingle
    {
        [JsonProperty("data")]
        public required JikanMangaData Data { get; set; }
    }

    public class JikanMangaData
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonProperty("title")]
        public required string Title { get; set; }

        [JsonProperty("title_english")]
        public required string EnglishTitle { get; set; }

        [JsonProperty("chapters")]
        public int? Chapters { get; set; }

        [JsonProperty("volumes")]
        public int? Volumes { get; set; }

        [JsonProperty("score")]
        public double? Score { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("rank")]
        public string? Rank { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("genres")]
        public List<Genre>? Genres { get; set; }

        [JsonProperty("synopsis")]
        public string? Synopsis { get; set; }

        [JsonProperty("background")]
        public string? Background { get; set; }

        [JsonProperty("images")]
        public required Images Images { get; set; }

        [JsonProperty("aired")]
        public Aired? Aired { get; set; }

        [JsonProperty("authors")]
        public List<Authors>? Authors { get; set; }
    }
    public class Authors
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
}
