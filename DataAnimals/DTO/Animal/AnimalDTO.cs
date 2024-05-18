namespace DataAnimals.DTO.Animal
{
    public class AnimalDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float AgeAvg { get; set; }
        public List<string> filename { get; set; }
    }
}
