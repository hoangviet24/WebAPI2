namespace DataAnimals.Models
{
    public class AnimalImage
    {
        public int Id { get; set; }
        public int? Animal_Id { get; set; }
        public Animal? Animal { get; set; }
        public int? Image_Id { get; set; }
        public Image? Image { get; set; }
    }
}
