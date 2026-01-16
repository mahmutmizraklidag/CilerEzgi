using System.ComponentModel.DataAnnotations;

namespace CilerEzgi.Entities
{
    public class WhyChooseUs
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Title { get; set; }
        [Display(Name = "Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Description { get; set; }
    }
}
