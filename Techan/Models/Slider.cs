
using System.ComponentModel.DataAnnotations.Schema;

namespace Techan.Models
{
    public class Slider : BaseEntity
    {
        public string ImagePath { get; set; }
        public string LittleTitle { get; set; }
        public string Title { get; set; }
        public string BigTitle { get; set; }
        public string Offer { get; set; }
        public string Link { get; set; }
    }
}
