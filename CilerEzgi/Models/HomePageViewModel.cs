using CilerEzgi.Entities;

namespace CilerEzgi.Models
{
    public class HomePageViewModel
    {
        public About About { get; set; }
        public List<Service> Services { get; set; }
        public List<Pricing> Pricings { get; set; }
    }
}
