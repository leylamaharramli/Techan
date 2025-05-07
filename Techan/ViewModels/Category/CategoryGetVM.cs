using System.ComponentModel.DataAnnotations;

namespace Techan.ViewModels.Category
{
    public class CategoryGetVM
    {
        public int Id { get; set; }
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; } = null!;
    }
}
