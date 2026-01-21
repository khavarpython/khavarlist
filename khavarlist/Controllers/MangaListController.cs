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
    public class MangaListController : Controller
    {
        private readonly IMangaService _mangaService;
        private readonly UserManager<User> _userManager;

        public MangaListController(IMangaService mangaService, UserManager<User> userManager)
        {
            _mangaService = mangaService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return View();
            }
            var userMangas = await _mangaService.GetUserMangas(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToList(int mangaId, string readStatus)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Json(new { success = false, message = "No user" });
                }
                else
                {
                    await _mangaService.AddMangaToList(userId, mangaId, readStatus);
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
        public async Task<IActionResult> UpdateStatus(int mangaId, string readStatus)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Json(new { success = false, message = "No User" });
                }
                else
                {
                    await _mangaService.UpdateMangaStatus(userId, mangaId, readStatus);
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
        public async Task<IActionResult> UpdateProgress(int mangaId, int progress)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId != null)
                {
                    await _mangaService.UpdateMangaProgress(userId, mangaId, progress);
                    return Json(new { success = true, message = "Progress updated!" });
                }
                else
                {
                    return Json(new { success = false, message = "No User" });
                }


            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateScore(int mangaId, int score)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId != null)
                {
                    await _mangaService.UpdateMangaScore(userId, mangaId, score);
                    return Json(new { success = true, message = "Score updated!" });
                }
                else
                {
                    return Json(new { success = false, message = "No User" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
    }
}
