using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class AboutPage
    {
        [Key]
        public int AboutId { get; set; }

        [Required(ErrorMessage = "Header Image is required.")]
        [Display(Name = "Header Image")]
        public string ImageHeader { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image in bottom layer is required.")]
        [Display(Name = "Image in bottom layer")]
        public string ImageBottom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image on top layer is required.")]
        [Display(Name = "Image on top layer")]
        public string ImageTop { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Description is required.")]
        [Display(Name = "Company Description")]
        public string CompanyDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mission Title is required.")]
        [Display(Name = "Mission Title")]
        public string MissionTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mission Description is required.")]
        [Display(Name = "Mission Description")]
        public string MissionDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mission Background Color is required.")]
        [Display(Name = "Mission Background Color")]
        public string MissionBgColor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vision Title is required.")]
        [Display(Name = "Vision Title")]
        public string VisionTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vision Description is required.")]
        [Display(Name = "Vision Description")]
        public string VisionDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vision Background Color is required.")]
        [Display(Name = "Vision Background Color")]
        public string VisionBgColor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us Section Title is required.")]
        [Display(Name = "Why Us Section Title")]
        public string WhyUsSectionTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us Section Background Color is required.")]
        [Display(Name = "Why Us Section Background Color")]
        public string WhyUsSectionBgColor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 1 Title is required.")]
        [Display(Name = "Why Us 1 Title")]
        public string WhyUs1Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 1 Description is required.")]
        [Display(Name = "Why Us 1 Description")]
        public string WhyUs1Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 1 Background Color is required.")]
        [Display(Name = "Why Us 1 Background Color")]
        public string WhyUs1BgColor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 2 Title is required.")]
        [Display(Name = "Why Us 2 Title")]
        public string WhyUs2Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 2 Description is required.")]
        [Display(Name = "Why Us 2 Description")]
        public string WhyUs2Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 2 Background Color is required.")]
        [Display(Name = "Why Us 2 Background Color")]
        public string WhyUs2BgColor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 3 Title is required.")]
        [Display(Name = "Why Us 3 Title")]
        public string WhyUs3Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 3 Description is required.")]
        [Display(Name = "Why Us 3 Description")]
        public string WhyUs3Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Why Us 3 Background Color is required.")]
        [Display(Name = "Why Us 3 Background Color")]
        public string WhyUs3BgColor { get; set; } = string.Empty;

        
    }
}
