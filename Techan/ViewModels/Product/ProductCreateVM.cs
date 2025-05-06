using System.ComponentModel.DataAnnotations;

namespace Techan.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
