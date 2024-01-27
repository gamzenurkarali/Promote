using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class Layout
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Logo Path is required.")]
        [Display(Name = "Logo Path")]
        public string LogoPath { get; set; }

        [Required(ErrorMessage = "Footer Color is required.")]
        [Display(Name = "Footer Color")]
        public string FooterColor { get; set; }

        [Required(ErrorMessage = "Highlight Color is required.")]
        [Display(Name = "Highlight Color")]
        public string HighlightColor { get; set; }
    }
}
