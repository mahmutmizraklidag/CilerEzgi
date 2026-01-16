using CilerEzgi.Data;
using CilerEzgi.Entities; // Pricing modelinin olduğu namespace
using CilerEzgi.Models;   // Card modelinin olduğu namespace
using Microsoft.AspNetCore.Mvc;

namespace CilerEzgi.Controllers
{
    public class PayController : Controller
    {
        private readonly DatabaseContext _context;

        public PayController(DatabaseContext context)
        {
            _context = context;
        }

        // Sayfayı Getiren Metot
        [HttpGet]
        public IActionResult Index()
        {
            // Burada normalde bir Pricing modeli gönderiyorsunuz.
            // Örnek: var model = _context.Pricing.FirstOrDefault();
            // return View(model);
            return View();
        }

        // AJAX ile Post Edilen Metot
        [HttpPost]
        public IActionResult Index(Card model)
        {
            if (!ModelState.IsValid)
            {
                // Form validasyonu başarısızsa hata dön
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Lütfen bilgileri eksiksiz doldurunuz.", errors = errors });
            }

            try
            {
                // NOT: Gerçek bir ödeme sisteminde (Iyzico, Stripe vb.) kredi kartı bilgileri
                // veritabanına KAYDEDİLMEZ. Direkt ödeme servisine gönderilir.
                // Aşağıdaki kod sadece veriyi aldığınızı simüle eder.

                // Örnek: Gelen Pricing ID'sini (model.Id) kullanarak işlem yapabilirsiniz.
                // var packageId = model.Id; 

                // İşlemlerinizi burada yapın...
                // _context.Orders.Add(new Order { ... });
                // _context.SaveChanges();

                return Json(new { success = true, message = "Ödeme başarılı." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "İşlem sırasında bir hata oluştu: " + ex.Message });
            }
        }
    }
}