using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace CilerEzgi.Validations
{
    public class LuhnCreditCardAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            // Boşlukları temizle
            string cardNumber = value.ToString().Replace(" ", "").Replace("-", "");

            if (!cardNumber.All(char.IsDigit))
                return new ValidationResult("Kart numarası sadece rakamlardan oluşmalıdır.");

            if (cardNumber.Length < 13 || cardNumber.Length > 19)
                return new ValidationResult("Kart numarası uzunluğu geçersiz.");
            if (Regex.IsMatch(cardNumber, @"^8+$"))
            {
                return new ValidationResult("Geçersiz kredi kartı numarası.");

            }

            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                char[] nx = cardNumber.ToArray();
                int n = int.Parse(nx[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n = (n % 10) + 1;
                }
                sum += n;
                alternate = !alternate;
            }

            if (sum % 10 == 0)
                return ValidationResult.Success;

            return new ValidationResult("Geçersiz kredi kartı numarası.");
        }
    }
}