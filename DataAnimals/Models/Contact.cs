using System.ComponentModel.DataAnnotations;

namespace DataAnimals.Models
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public int? PhoneNumber { get; set; }
        public string Description { get; set; }
    }
}
