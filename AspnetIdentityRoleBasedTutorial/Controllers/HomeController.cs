using AspnetIdentityRoleBasedTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspnetIdentityRoleBasedTutorial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult DonateMoney()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Important()
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