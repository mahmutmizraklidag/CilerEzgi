using System.ComponentModel.DataAnnotations;

namespace CilerEzgi.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [Display(Name = "Hizmet Başlığı"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Title { get; set; }
        [Display(Name = "Hizmet Açıklaması"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Description { get; set; }
        [Display(Name = "Hizmet Görseli")]
        public string? Image { get; set; }
    }
}
