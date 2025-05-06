using System.ComponentModel.DataAnnotations.Schema;

namespace Techan.ViewModels.Sliders
{
    public class SliderUpdateVM
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public string LittleTitle { get; set; }
        public string Title { get; set; }
        public string BigTitle { get; set; }
        public string Offer { get; set; }
        public string Link { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
