using DataAnimals.Models;

namespace DataAnimals.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string>? Animal_Name { get; set; }
    }
}
