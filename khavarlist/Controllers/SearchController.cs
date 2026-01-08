using khavarlist.Areas.Identity.Data;
using khavarlist.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace khavarlist.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApiController _apiController;
        private readonly UserManager<User> _userManager;
        private readonly IAnimeService _animeService;
        public SearchController(IHttpClientFactory httpClientFactory, UserManager<User> userManager, IAnimeService animeService)
        {
            _apiController = new ApiController(httpClientFactory);
            _animeService = animeService;
            _userManager = userManager;
        }

        [Route("Search")]
        public async Task<IActionResult> Index(string query, int id = 1)
        {
            if (string.IsNullOrEmpty(query))
            {
                query = "";
            }

            var animes = await _apiController.GetAnimes(query, id);

            // Pass query to view for pagination links
            ViewBag.Query = query;

            return View(animes);
        }
    }
}
