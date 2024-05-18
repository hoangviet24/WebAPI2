using System.ComponentModel.DataAnnotations;

namespace DataAnimals.DTO
{
    public class ImageRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
