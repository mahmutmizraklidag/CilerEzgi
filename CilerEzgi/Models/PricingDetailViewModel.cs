using CilerEzgi.Entities;

namespace CilerEzgi.Models
{
    public class PricingDetailViewModel
    {
        public Pricing MainPricing { get; set; }
        public List<Pricing> SubPricings { get; set; }
    }
}
