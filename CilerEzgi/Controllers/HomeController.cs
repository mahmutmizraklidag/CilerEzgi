using System.Diagnostics;
using CilerEzgi.Data;
using CilerEzgi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CilerEzgi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;
        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var HomePageViewModel = new HomePageViewModel
            {
                About = _context.Abouts.FirstOrDefault(),
                Services = _context.Services.ToList(),
                Pricings = _context.Pricings.ToList()
            };
            return View(HomePageViewModel);
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
