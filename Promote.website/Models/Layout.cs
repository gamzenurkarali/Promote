using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class Layout
    {
        [Key]
        public int Id { get; set; }
         
        [Display(Name = "Logo Path")]
        public string LogoPath { get; set; }
         
        [Display(Name = "Footer Color")]
        public string FooterColor { get; set; }
         
        [Display(Name = "Highlight Color")]
        public string HighlightColor { get; set; }

        [Display(Name = "Social Media 1 Link")]
        public string SocialMedia1Link { get; set; }

        [Display(Name = "Social Media 1 Icon")]
        public string SocialMedia1Icon { get; set; }

        [Display(Name = "Social Media 2 Link")]
        public string SocialMedia2Link { get; set; }

        [Display(Name = "Social Media 2 Icon")]
        public string SocialMedia2Icon { get; set; }

        [Display(Name = "Social Media 3 Link")]
        public string SocialMedia3Link { get; set; }

        [Display(Name = "Social Media 3 Icon")]
        public string SocialMedia3Icon { get; set; }

        [Display(Name = "Social Media 4 Link")]
        public string SocialMedia4Link { get; set; }

        [Display(Name = "Social Media 4 Icon")]
        public string SocialMedia4Icon { get; set; }
    }
}
