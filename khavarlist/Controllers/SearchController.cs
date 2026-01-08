using khavarlist.Areas.Identity.Data;
using khavarlist.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace khavarlist.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApiController _apiController;

        public SearchController(IHttpClientFactory httpClientFactory)
        {
            _apiController = new ApiController(httpClientFactory);
        }

        [Route("Search")]
        public async Task<IActionResult> Index(string query, int id = 1)
        {
            if (string.IsNullOrEmpty(query))
            {
                query = "";
            }

            var animes = await _apiController.GetAnimes(query, id);
            ViewBag.Query = query;

            return View("AnimeSearch", animes);
        }
    }
}
