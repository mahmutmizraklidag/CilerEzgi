using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CilerEzgi.Data;
using CilerEzgi.Entities; // Pricing modelinin olduğu namespace
using CilerEzgi.Models;   // Card modelinin olduğu namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CilerEzgi.Controllers
{
    public class PayController : Controller
    {


        private readonly DatabaseContext _context;

        private string merchant_id = "306152";
        private string merchant_key = "L2nusuEkYJjgSwRL";
        private string merchant_salt = "wUxTh8Z9ccbft1hd";
        private string bizimhesap_firm_id = "136F4C8BD9AE410A9E1255DF2E57128B";

        static string GenerateRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

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
        [HttpPost("/pay")]
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

                var product = _context.Pricings
                    .FirstOrDefault(x => x.Id == model.Id);
                // ✅ Toplam fiyata indirimli halini yaz (kargo dahil)
                double payment_amount = double.Parse(product.Price); // Kuruş cinsinden
                if (payment_amount < 0) payment_amount = 0;

                string merchant_oid = GenerateRandomString(30) + new Random().Next(0, 9999999) + new Random().Next(0, 9999999);

                string user_name = model.Name + " " + model.Surname;
                string user_address = $"{model.City}/{model.District} {model.Address}";
                string user_phone = model.Phone;
                string email = model.Email;
                string user_ip = HttpContext?.Connection.RemoteIpAddress.ToString();
                string card_type = "bonus";

                int debug_on = 1;
                string test_mode = "0";
                string non_3d = "0";
                string non3d_test_failed = "0";
                int installment_count = 0;
                string no_installment = "0";
                string max_installment = "0";
                string payment_type = "card";
                string post_url = "https://www.paytr.com/odeme";
                string currency = "TL";
                string lang = "tr";
                string merchant_ok_url = "https://kimdemisyapamazsin.com//payment/success";
                string merchant_fail_url = "https://kimdemisyapamazsin.com//payment/failed";
                string expiry_month = model.ExpirationDate.Split("/")[0];
                string expiry_year = model.ExpirationDate.Split("/")[1];
                string cc_owner = model.CardHolderName;
                string card_number = model.CardNumber;
                string cvv = model.CVV;

                var user_basket = new List<object[]>()
            {
                new object[]
                {
                    product.Title,
                    payment_amount,
                    1
                }
            };


                string user_basket_json = System.Text.Json.JsonSerializer.Serialize(user_basket);

                string Birlestir = string.Concat(merchant_id, user_ip, merchant_oid, email, payment_amount.ToString(), payment_type, installment_count, currency, test_mode, non_3d, merchant_salt);
                HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
                byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(Birlestir));
                var token = Convert.ToBase64String(b);

                //sipariş oluşturma işlemi
                var order = new Order()
                {

                    Price = payment_amount.ToString(),
                    Fullname = user_name,
                    Phone = user_phone,
                    TcId = model.IdentificationNumber,
                    Email = email,
                    Address = user_address,
                    City = model.City,
                    District = model.District,
                    PricingName = product.Id,
                    OrderNumber = merchant_oid,
                    IsPay = false
                };



                _context.Add(order);
                _context.SaveChanges();

                // ✅ Geriye dönen değerlere karışmadan aynen bırakıldı
                return Ok(new
                {
                    merchant_id,
                    merchant_key,
                    email,
                    payment_amount,
                    merchant_oid,
                    user_name,
                    user_address,
                    user_phone,
                    user_ip,
                    card_type,
                    debug_on,
                    test_mode,
                    non_3d,
                    non3d_test_failed,
                    installment_count,
                    no_installment,
                    max_installment,
                    payment_type,
                    post_url,
                    currency,
                    lang,
                    merchant_fail_url,
                    merchant_ok_url,
                    expiry_month,
                    expiry_year,
                    cc_owner,
                    card_number,
                    cvv,
                    user_basket = user_basket_json,
                    paytr_token = token
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "İşlem sırasında bir hata oluştu: " + ex.Message });
            }
        }

        [HttpPost("/payment/notification")]
        public async Task<string> PaymentNotification(PaytrRequestDTO? request)
        {
            try
            {
                if (request != null)
                {
                    string Birlestir = string.Concat(request.merchant_oid, merchant_salt, request.status, request.total_amount);
                    HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
                    byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(Birlestir));
                    string token = Convert.ToBase64String(b);

                    if (request.hash.ToString() != token)
                    {
                        return "OK";
                    }

                    if (request.status == "success")
                    {
                        var order = _context.Orders.FirstOrDefault(x => x.OrderNumber == request.merchant_oid);
                        if (order != null)
                        {

                            // ✅ Aynı siparişe iki kez bildirim gelirse kupon iki kez düşmesin
                            if (!order.IsPay)
                            {
                                order.IsPay = true;
                                _context.Update(order);
                                _context.SaveChanges();
                            }
                            var url = "https://bizimhesap.com/api/b2b/addinvoice";
                            var taxPrice = double.Parse(order.Price) * 0.20;
                            var requestBody = new
                            {
                                firmId = bizimhesap_firm_id,
                                invoiceNo = order.Id,
                                invoiceType = 3,
                                note = $"{order.PricingName} Online Paket Satışı",
                                dates = new
                                {
                                    invoiceDate = DateTime.Now,
                                    dueDate = DateTime.Now,
                                    deliveryDate = DateTime.Now
                                },
                                customer = new
                                {
                                    customerId = order.Id,
                                    title = order.Fullname,
                                    taxNo = order.TcId,
                                    email = order.Email,
                                    phone = order.Phone,
                                    address = $"{order.City} / {order.District} ----- {order.Address}"
                                },
                                amounts = new
                                {
                                    currency = "TL",
                                    gross = double.Parse(order.Price) - taxPrice,
                                    discount = "0",
                                    net = double.Parse(order.Price) - taxPrice,
                                    tax = taxPrice,
                                    total = order.Price
                                },
                                details = new[]
                                {
                                    new{
                                        productId = order.PricingName,
                                        productName = order.PricingName,
                                        note = $"{order.PricingName} adındaki paket",
                                        taxRate = "20.00",
                                        quantity = 1,
                                        unitPrice = double.Parse(order.Price) - taxPrice,
                                        grossPrice = double.Parse(order.Price) - taxPrice,
                                        discount = "0.00",
                                        net = double.Parse(order.Price) - taxPrice,
                                        tax = taxPrice,
                                        total = order.Price
                                    }
                                }
                            };

                            var json = JsonSerializer.Serialize(requestBody);

                            using var client = new HttpClient();
                            using var content = new StringContent(json, Encoding.UTF8, "application/json");

                            // Eğer token gerekiyorsa:
                            // client.DefaultRequestHeaders.Authorization =
                            //     new AuthenticationHeaderValue("Bearer", "TOKEN_BURAYA");

                            var response = await client.PostAsync(url, content);

                            var body = await response.Content.ReadAsStringAsync();

                            if (!response.IsSuccessStatusCode)
                            {
                                // fatura oluşturuldu
                            }


                        }
                    }
                    else
                    {
                        // bildirilmeli
                    }
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return "OK";
            }
        }

        [HttpGet("/payment/success")]
        public IActionResult PaymentSuccessNotification()
        {
            return View("Success");
        }

        [HttpPost("/payment/failed")]
        public IActionResult PaymentFailedNotification([FromForm] PaytrRequestDTO? request)
        {
            return View("Failed");
        }

        // =========================
        // ✅ Cart Index'ten uyarlanan kupon + totals helper'ları
        // =========================

    }
}



