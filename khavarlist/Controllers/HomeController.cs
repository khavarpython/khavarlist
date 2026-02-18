using khavarlist.Areas.Identity.Data;
using khavarlist.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace khavarlist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiController _apiController;
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager, IHttpClientFactory httpClientFactory)
        {
            this._userManager = userManager;
            _apiController = new ApiController(httpClientFactory);
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeView
            {
                TopAnime = (await _apiController.GetTopAnime())?.Data,
                TopManga = (await _apiController.GetTopManga())?.Data,
                CurrentAnime = (await _apiController.GetCurrentAnimes())?.Data,
                AnimeRecs= (await _apiController.GetRecommendations("anime"))?.Data,
                MangaRecs=(await _apiController.GetRecommendations("manga"))?.Data
            };

            return View("Home", viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}