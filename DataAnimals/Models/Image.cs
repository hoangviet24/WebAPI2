using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnimals.Models
{
    public class Image
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long Size { get; set; }
        public string FilePath { get; set; }
        public int AnimalImage_Id { get;set; }
        public List<AnimalImage> AnimalImages { get; set; }
    }
}
