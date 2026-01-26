using CilerEzgi.Data;
using CilerEzgi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CilerEzgi.Controllers
{
    public class PricingController : Controller
    {
        private readonly DatabaseContext _context;

        public PricingController(DatabaseContext context)
        {
            _context = context;
        }
        [Route("paketler/{slug?}")]
        public IActionResult Index(string slug)
        {
            // 1. Ana paketi bul
            var pricing = _context.Pricings.FirstOrDefault(x => x.Slug == slug);

            if (pricing == null)
            {
                return NotFound();
            }

            // 2. ViewModel'i doldur
            var viewModel = new PricingDetailViewModel
            {
                MainPricing = pricing,
                SubPricings = _context.Pricings
                                      .Where(x => x.ParentId == pricing.Id)
                                      .ToList()
            };

            // 3. ViewModel'i View'a gönder
            return View(viewModel);
        }
    }
}
