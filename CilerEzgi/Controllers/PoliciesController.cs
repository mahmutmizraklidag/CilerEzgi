using CilerEzgi.Data;
using Microsoft.AspNetCore.Mvc;

namespace CilerEzgi.Controllers
{
    public class PoliciesController : Controller
    {
        private readonly DatabaseContext _context;

        public PoliciesController(DatabaseContext context)
        {
            _context = context;
        }
        [Route("politikalarimiz/{slug}")]
        public IActionResult Index(string slug)
        {
            var model = _context.Policies.FirstOrDefault(p => p.Slug == slug);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
