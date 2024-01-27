using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class Sublink
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "LogoPath is required.")]
        [Display(Name = "Logo Path")]
        public string LogoPath { get; set; }

        [Required(ErrorMessage = "Page Name is required.")]
        [Display(Name = "Page Name")]
        public string PageName { get; set; }
    }
}
