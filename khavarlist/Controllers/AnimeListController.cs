using khavarlist.Areas.Identity.Data;
using khavarlist.Models;
using khavarlist.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace khavarlist.Controllers
{
    [Authorize]
    public class AnimeListController : Controller
    {
        private readonly IAnimeService _animeService;
        private readonly UserManager<User> _userManager;

        public AnimeListController(IAnimeService animeService, UserManager<User> userManager)
        {
            _animeService = animeService;
            _userManager = userManager; 
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return View();
            }
            var userAnimes = await _animeService.GetUserAnimes(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToList(int animeId, string WatchStatus)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Json(new { sucess = false, message = "No user" });
                }
                else
                {
                    await _animeService.AddAnimeToList(userId, animeId, WatchStatus);
                    return Json(new { success = true, message = "Added to list successfully!" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int animeId, string watchStatus)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Json(new { sucess = false,message="No User" });
                }
                else
                {
                    await _animeService.UpdateAnimeStatus(userId, animeId, watchStatus);
                    return Json(new { success = true, message = "Status updated!" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProgress(int animeId, int progress,string duration)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId != null)
                {
                    await _animeService.UpdateAnimeProgress(userId, animeId, progress, duration);
                    return Json(new { success = true, message = "Progress updated!" });
                }
                else
                {
                    return Json(new { sucess = false, message = "No User" });
                }
                
              
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateScore(int animeId, int score)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId != null)
                {
                    await _animeService.UpdateAnimeScore(userId, animeId, score);
                    return Json(new { success = true, message = "Score updated!" });
                }
                else
                {
                    return Json(new { sucess = false, message = "No User" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
    }
}
