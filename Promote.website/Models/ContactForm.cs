using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class ContactForm
    {
        public int ContactFormId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required.")]
        [Display(Name = "Message")]
        public string Message { get; set; } = string.Empty;
    }
}
