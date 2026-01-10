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
        private readonly IMangaService _mangaService;

        public ShowController(IHttpClientFactory httpClientFactory, UserManager<User> userManager, IAnimeService animeService, IMangaService mangaService)
        {
            _apiController = new ApiController(httpClientFactory);
            _animeService = animeService;
            _userManager = userManager;
            _mangaService = mangaService;
        }

        [Route("Show/Anime/{id}")]
        public async Task<IActionResult> Anime(int id)
        {
            var anime = await _apiController.GetAnimeById(id);
            var userId = _userManager.GetUserId(User);

            if(userId == null)
            {
                return NotFound();
            }

            if (anime == null)
            {
                return NotFound();
            }

            ViewData["AddStatus"] = await _animeService.IsAnimeInUserList(userId, id);
            ViewData["UserDetails"] = await _animeService.GetUserAnimeDetails(userId, id);

            return View(anime);
        }

        [Route("Show/Manga/{id}")]
        public async Task<IActionResult> Manga(int id)
        {
            var manga = await _apiController.GetMangaById(id);
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return NotFound();
            }

            if (manga == null)
            {
                return NotFound();
            }

            ViewData["AddStatus"] = await _mangaService.IsMangaInUserList(userId, id);
            ViewData["UserDetails"] = await _mangaService.GetUserMangaDetails(userId, id);
            return View(manga);
        }
    }
}
