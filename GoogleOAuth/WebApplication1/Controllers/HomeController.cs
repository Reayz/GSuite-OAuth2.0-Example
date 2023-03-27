using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            GSuiteUserData userData = new GSuiteUserData();

            if (principal != null && principal.Claims != null)
            {
                userData.UserId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                userData.FullName = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                userData.Email = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                userData.UserImageUrl = principal.Claims.FirstOrDefault(x => x.Type == "image").Value;
            }

            return View(userData);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
