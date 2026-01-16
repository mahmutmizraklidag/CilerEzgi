using System.ComponentModel.DataAnnotations;
using CilerEzgi.Validations; // Özel validasyon sınıflarının olduğu yer

namespace CilerEzgi.Models
{
    public class Card
    {
        public int Id { get; set; }

        public string? Price { get; set; }

        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Kart numarası zorunludur.")]
        [LuhnCreditCard(ErrorMessage = "Geçersiz kredi kartı numarası.")] // Özel Yazdığımız Attribute
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Kart sahibi ismi zorunludur.")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]*$", ErrorMessage = "İsim sadece harflerden oluşmalıdır.")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Son kullanma tarihi zorunludur.")]
        [RegularExpression(@"(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Format AA/YY şeklinde olmalıdır.")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "CVV zorunludur.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV 3 haneli olmalıdır.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Sadece rakam giriniz.")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Adınız zorunludur.")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]*$", ErrorMessage = "Geçersiz karakter.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadınız zorunludur.")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]*$", ErrorMessage = "Geçersiz karakter.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [RegularExpression(@"^05[0-9]{9}$", ErrorMessage = "Telefon 05xxxxxxxxx formatında olmalıdır.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur.")]
        [TcKimlikNo(ErrorMessage = "Geçersiz TC Kimlik Numarası.")] // Özel Yazdığımız Attribute
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Şehir seçimi zorunludur.")]
        public string City { get; set; }

        [Required(ErrorMessage = "İlçe bilgisi zorunludur.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Adres bilgisi zorunludur.")]
        [MinLength(10, ErrorMessage = "Adres çok kısa, lütfen detaylandırın.")]
        public string Address { get; set; }
    }
}