using khavarlist.Areas.Identity.Data;
using khavarlist.Models;
using khavarlist.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace khavarlist.Controllers
{
    public class ShowController : Controller
    {
        private readonly ApiController _apiController;
        private readonly UserManager<User> _userManager;
        private readonly IAnimeService _animeService;
        public ShowController(IHttpClientFactory httpClientFactory, UserManager<User> userManager, IAnimeService animeService)
        {
             _apiController = new ApiController(httpClientFactory);
            _animeService = animeService;
            _userManager = userManager;
        }


        [Route("Show/Anime/{id}")]
        public async Task<IActionResult> Anime(int id)
        {
            var anime = await _apiController.GetAnimeById(id);
            var userId = _userManager.GetUserId(User);

            ViewData["AddStatus"] = await _animeService.IsAnimeInUserList(userId, id);
            ViewData["UserDetails"] = await _animeService.GetUserAnimeDetails(userId, id);
          
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
