using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class İnformationForm
    {
        public int İnformationFormId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
    }
}
