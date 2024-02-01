using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class Sublink
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Page Path is required.")]
        [Display(Name = "Page Path")]
        public string PagePath { get; set; }

        [Required(ErrorMessage = "Page Name is required.")]
        [Display(Name = "Page Name")]
        public string PageName { get; set; }
    }
}
