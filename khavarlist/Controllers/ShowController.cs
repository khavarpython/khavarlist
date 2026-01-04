using Microsoft.AspNetCore.Mvc;

namespace khavarlist.Controllers
{
    public class ShowController : Controller
    {
        private readonly ApiController _apiController;
        public ShowController(IHttpClientFactory httpClientFactory)
        {
             _apiController = new ApiController(httpClientFactory);
        }
        [Route("Show/Anime/{id}")]
        public async Task<IActionResult> Anime(int id)
        {
            var anime = await _apiController.GetAnimeById(id);

            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        [Route("Show/Manga/{id}")]
        public async Task<IActionResult> Manga(int id)
        {
            var manga = await _apiController.GetMangaById(id);

            if (manga == null)
            {
                return NotFound();
            }
            return View(manga);
        }
    }
}
