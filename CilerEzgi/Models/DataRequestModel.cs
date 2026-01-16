using CilerEzgi.Entities;

namespace CilerEzgi.Models
{
    public class DataRequestModel
    {
        public static SiteSetting SiteSetting { get; set; } = new SiteSetting();
        public static List<Policies> Policies { get; set; } = new List<Policies>();
        public static void ClearData()
        {
            
            SiteSetting = new SiteSetting();     // null yapma
            Policies = new List<Policies>();
        }
    }
}
