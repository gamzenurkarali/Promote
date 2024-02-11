using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class InformationForm
    {
        public int InformationFormId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "EmailAdress")]
        public string EmailAdress { get; set; } = string.Empty;
    }
}
