using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using khavarlist.Models;
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
        // GET TOP ANIMES
        public async Task<JikanAnimeList?> GetTopAnime()
        {
            try
            {
                var url = "https://api.jikan.moe/v4/top/anime";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JikanAnimeList>(content);
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

        // GET TOP MANGAS
        public async Task<JikanMangaList?> GetTopManga()
        {
            try
            {
                var url = "https://api.jikan.moe/v4/top/manga";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JikanMangaList>(content);
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

        // GET ANIME BY ID
        public async Task<JikanAnimeSingle?> GetAnimeById(int id)
        {
            try
            {
                var url = $"https://api.jikan.moe/v4/anime/{id}/full";
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JikanAnimeSingle>(content);
                    return data; 
                }
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        // GET MANGA BY ID
        public async Task<JikanMangaSingle?> GetMangaById(int id)
        {
            try
            {
                var url = $"https://api.jikan.moe/v4/manga/{id}/full";
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JikanMangaSingle>(content);
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
}
