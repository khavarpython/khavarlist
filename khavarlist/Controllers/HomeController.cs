using System.Diagnostics;
using khavarlist.Areas.Identity.Data;
using khavarlist.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace khavarlist.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager )
        {
                this._userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewData["UserID"] =_userManager.GetUserId(this.User);
            return View();
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
