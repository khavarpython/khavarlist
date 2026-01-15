using khavarlist.Areas.Identity.Data;
using khavarlist.Models;
using khavarlist.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace khavarlist.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAnimeService _animeService;
        private readonly IMangaService _mangaService;
        private readonly UserManager<User> _userManager;

        public ProfileController(IAnimeService animeService, IMangaService mangaService, UserManager<User> userManager)
        {
            _animeService = animeService;
            _mangaService = mangaService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var viewModel = new ProfileView
            {
                UserMangas = await _mangaService.GetUserMangas(userId),
                UserAnimes = await _animeService.GetUserAnimes(userId),
                AnimeStats = await _animeService.GetUserAnimeStats(userId),
               // MangaStats = await _animeService.GetUserMangaStats(userId)
            };
            return View("Profile", viewModel);
        }
    }
}
