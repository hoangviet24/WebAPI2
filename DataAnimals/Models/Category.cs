using System.ComponentModel.DataAnnotations;

namespace DataAnimals.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CatergoryAnimal_Id { get; set; }
        public List<AnimalCategory>? AnimalCategory { get; set; }
    }
}
