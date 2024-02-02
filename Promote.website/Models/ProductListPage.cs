using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class ProductListPage
    {
        [Key]
        public int Id { get; set; }

        
        [Display(Name = "Header Image")]
        public string ImageHeader { get; set; } = string.Empty;

        
    }
}
