using System.ComponentModel.DataAnnotations;

namespace DataAnimals.Models
{
    public class AnimalCategory
    {
        [Key]
        public int? Id { get; set; }
        public int? Animal_Id { get; set; }
        public Animal? animal {  get; set; }
        public int? Category_Id { get; set; }
        public Category? category { get; set; }
    }
}
