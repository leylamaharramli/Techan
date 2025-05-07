using System.ComponentModel.DataAnnotations.Schema;

namespace Techan.ViewModels.Brand
{
    public class BrandUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
