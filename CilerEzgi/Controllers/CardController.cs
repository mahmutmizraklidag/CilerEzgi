using CilerEzgi.Data;
using Microsoft.AspNetCore.Mvc;

namespace CilerEzgi.Controllers
{
    public class CardController : Controller
    {
        private readonly DatabaseContext _context;

        public CardController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var model=_context.Pricings.Find(id);
            var parentModel=_context.Pricings.Find(model.ParentId);
            ViewBag.ParentTitle=parentModel.Title;
            return View(model);
        }
    }
}
