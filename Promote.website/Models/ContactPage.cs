using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class ContactPage
    {
        public int ContactPageId { get; set; }

        [Required(ErrorMessage = "Header Image is required.")]
        [Display(Name = "Header Image")]
        public string ImageHeader { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact Info Title is required.")]
        [Display(Name = "Contact Info Title")]
        public string ContactInfoTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact Info Description is required.")]
        [Display(Name = "Contact Info Description")]
        public string ContactInfoDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Map Iframe URL is required.")]
        [Display(Name = "Map Iframe URL")]
        public string MapIframeUrl { get; set; } = string.Empty;
    }
}
