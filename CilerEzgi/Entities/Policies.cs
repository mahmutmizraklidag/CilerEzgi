using System.ComponentModel.DataAnnotations;

namespace CilerEzgi.Entities
{
    public class Policies
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), Required(ErrorMessage = "Başlık alanı zorunludur.")]
        public string Title { get; set; }
        [Display(Name = "Açıklama"), Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        public string Description { get; set; }
        [Display(Name = "Slug"), Required(ErrorMessage = "Slug alanı zorunludur.")]
        public string Slug { get; set; }
    }
}
