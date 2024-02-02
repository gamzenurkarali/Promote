using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    [Display(Name = "Product Name")]
    [Required(ErrorMessage = "Product Name is required.")]
    public string ProductName { get; set; }

    [Display(Name = "Product Image File Name")] 
    public string? ProductImageFileName { get; set; }

    [Required(ErrorMessage = "Fee is required.")]
    public decimal Fee { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Display(Name = "Tab 1 Description")]
    [Required(ErrorMessage = "Tab 1 Description is required.")]
    public string Tab1Description { get; set; }

    [Display(Name = "Tab 2 Description")]
    [Required(ErrorMessage = "Tab 2 Description is required.")]
    public string Tab2Description { get; set; }

    [Display(Name = "Tab 3 Description")]
    [Required(ErrorMessage = "Tab 3 Description is required.")]
    public string Tab3Description { get; set; }

    [Display(Name = "Detailed Description Background Image")] 
    public string? DetailedDescriptionBgImage { get; set; }

    [Display(Name = "Detailed Description")]
    [Required(ErrorMessage = "Detailed Description is required.")]
    public string DetailedDescription { get; set; }
}
