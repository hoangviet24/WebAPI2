using DataAnimals.Models;

namespace DataAnimals.DTO.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Animal_Name { get; set; }
    }
}
