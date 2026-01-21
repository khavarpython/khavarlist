using khavarlist.Areas.Identity.Data;
using khavarlist.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace khavarlist.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ApiController _apiController;

        public SearchController(IHttpClientFactory httpClientFactory)
        {
            _apiController = new ApiController(httpClientFactory);
        }
        public IActionResult Search(string searchType, string query)
        {
            if (searchType == "anime")
                return RedirectToAction("AnimeSearch", new { query });
            else
                return RedirectToAction("MangaSearch", new { query });
        }

        public async Task<IActionResult> AnimeSearch(string query, int id = 1)
        {
            if (string.IsNullOrEmpty(query))
            {
                query = "";
            }

            var animes = await _apiController.GetAnimes(query, id);
            ViewBag.Query = query;
            ViewBag.Type = "anime";

            return View("AnimeSearch", animes);
        }

        public async Task<IActionResult> MangaSearch(string query, int id = 1)
        {
            if (string.IsNullOrEmpty(query))
            {
                query = "";
            }

            var mangas = await _apiController.GetMangas(query, id);
            ViewBag.Query = query;
            return View("MangaSearch", mangas);
        }
    }
}
