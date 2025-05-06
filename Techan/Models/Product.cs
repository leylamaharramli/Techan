using System.ComponentModel.DataAnnotations;

namespace Techan.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0,100)]
        public int Discount { get; set; }
        public decimal  Price { get; set; }
        public string ImagePath { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public Brand? Brand { get; set; }
        public Category? Category { get; set; }
    }
}
