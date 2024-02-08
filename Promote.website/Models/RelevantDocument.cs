using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class RelevantDocument
    { 
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Creation date is required")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Update date is required")]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
    }

} 
