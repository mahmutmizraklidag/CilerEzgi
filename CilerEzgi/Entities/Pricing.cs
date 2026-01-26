using System.ComponentModel.DataAnnotations;

namespace CilerEzgi.Entities
{
    public class Pricing
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Title { get; set; }
        [Display(Name = "Fiyat")]
        public string? Price { get; set; }
        [Display(Name = "Slug")]
        public string? Slug { get; set; }
        [Display(Name = "Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Description { get; set; }

        [Display(Name = "Üst Kategori")]
        public int? ParentId { get; set; }
    }
}
