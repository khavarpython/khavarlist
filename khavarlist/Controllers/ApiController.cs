using khavarlist.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
namespace khavarlist.Controllers
{
    public class ApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public ApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // GET ALL ANIMES
        public async Task<JikanAnimeList?> GetAnimes(string? query, int page)
        {
            try
            {
                if (query == null) { query = ""; }
                var url = $"https://api.jikan.moe/v4/anime?q={query}&page={page}";
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

        // GET ALL MANGA
        public async Task<JikanMangaList?> GetMangas(string? query, int page)
        {
            try
            {
                if (query == null) { query = ""; }
                var url = $"https://api.jikan.moe/v4/manga?q={query}&page={page}";
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

        // GET CURRENT SEASON ANIMES
        public async Task<JikanAnimeList?> GetCurrentAnimes()
        {
            try
            {
                await Task.Delay(3000);
                var url = "https://api.jikan.moe/v4/seasons/now";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JikanAnimeList>(content);
                    return data;
                }
                return null;
            } catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        // GET ANIME RECOMMENDATIONS
        public async Task<JikanRecommendationList?> GetRecommendations(string type)
        {
            try
            {
                var url = $"https://api.jikan.moe/v4/recommendations/{type}";
                Console.WriteLine($"Exception: {url}");
                var response = await _httpClient.GetAsync(url);
                Console.WriteLine($"Exception: {response}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Reccs success");
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JikanRecommendationList>(content);
                    return data;
                }
                Console.WriteLine($"fail");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<JikanMangaCharacters?> GetMangaCharacters(int id)
        {
            try
            {
                
                var url = $"https://api.jikan.moe/v4/manga/{id}/characters";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JikanMangaCharacters>(content);
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


        // undecidicde
        public async Task<JikanAnimeList?> GetTopReviews()
        {
            try
            {
                var url = "https://api.jikan.moe/v4/top/reviews";
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

    }
}
