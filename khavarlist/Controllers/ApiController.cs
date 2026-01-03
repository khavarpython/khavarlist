using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
namespace khavarlist.Controllers
{
    public class ApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public ApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<JikanTopAnimeResponse?> GetTopAnime()
        {
            try
            {
                var url = "https://api.jikan.moe/v4/top/anime";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JikanTopAnimeResponse>(content);
                    return data;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
    }

        public class JikanTopAnimeResponse
    {
        [JsonProperty("data")]
        public required List<JikanAnimeData> Data { get; set; }

        [JsonProperty("pagination")]
        public required Pagination Pagination { get; set; }
    }

    public class JikanAnimeData
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonProperty("title")]
        public required string Title { get; set; }

        [JsonProperty("score")]
        public double? Score { get; set; }

        [JsonProperty("episodes")]
        public int? Episodes { get; set; }

        [JsonProperty("synopsis")]
        public string? Synopsis { get; set; }

        [JsonProperty("images")]
        public required Images Images { get; set; }

        [JsonProperty("aired")]
        public Aired? Aired { get; set; }
    }

    public class Images
    {
        [JsonProperty("jpg")]
        public required ImageUrls Jpg { get; set; }
    }

    public class ImageUrls
    {
        [JsonProperty("image_url")]
        public required string ImageUrl { get; set; }

        [JsonProperty("large_image_url")]
        public string? LargeImageUrl { get; set; }
    }

    public class Aired
    {
        [JsonProperty("from")]
        public DateTime? From { get; set; }

        [JsonProperty("to")]
        public DateTime? To { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("last_visible_page")]
        public int LastVisiblePage { get; set; }

        [JsonProperty("has_next_page")]
        public bool HasNextPage { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
    }
}
