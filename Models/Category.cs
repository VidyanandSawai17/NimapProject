using System.ComponentModel.DataAnnotations;

namespace NimapProject.Models
{
    public class Category
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "name is required")]
        public string CategoryName { get; set; }
    }
}
