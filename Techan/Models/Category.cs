﻿using System.ComponentModel.DataAnnotations;

namespace Techan.Models
{
    public class Category : BaseEntity
    {
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; } = null!;
        public IEnumerable<Product>? Products { get; set; }
    }
}
