using System.ComponentModel.DataAnnotations;

public class ProductDetailPage
{
    [Key]
    public int Id { get; set; }

    
    [Display(Name = "Header Image")]
    public string ImageHeader { get; set; } = string.Empty;

    [Display(Name = "is Tab Section Included")]
    [Required(ErrorMessage = "is Tab Section Included is required.")]
    public bool IsTabSectionIncluded { get; set; }

    [Display(Name = "Tab 1 Title")]
    [Required(ErrorMessage = "Tab 1 Title is required.")]
    public string Tab1Title { get; set; }

    [Display(Name = "Tab 2 Title")]
    [Required(ErrorMessage = "Tab 2 Title is required.")]
    public string Tab2Title { get; set; }

    [Display(Name = "Tab 3 Title")]
    [Required(ErrorMessage = "Tab 3 Title is required.")]
    public string Tab3Title { get; set; }

    [Display(Name = "Detailed Description Title")]
    [Required(ErrorMessage = "Detailed Description Title is required.")]
    public string DetailedDescriptionTitle { get; set; }
}
