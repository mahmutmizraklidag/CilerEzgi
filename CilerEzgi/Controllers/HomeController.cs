using System.Diagnostics;
using CilerEzgi.Data;
using CilerEzgi.Entities;
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
                Pricings = _context.Pricings.Where(x=>x.ParentId!=0).ToList()
            };
            return View(HomePageViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactForm contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.ContactForms.Add(contact);
                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        //var temp = MailTemplates.ContactFormTemplate(entity);
                        //MailSender mailSender = new MailSender();
                        //await mailSender.SendMailAsync(entity.Email, "Zoom Danýþmanlýk iletiþim Formu", temp, entity.Name);
                        return Json(new { success = true, message = "Mesajýnýz gönderildi." });
                    }
                }
                catch
                {
                    return Json(new { success = false, message = "Hata oluþtu!" });
                }
            }
            return Json(new { success = false, message = "Form bilgileri hatalý!" });
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
