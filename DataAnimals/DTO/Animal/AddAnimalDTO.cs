namespace DataAnimals.DTO.Animal
{
    public class AddAnimalDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? AgeAvg { get; set; }
        public List<int> file_Id { get; set; }
    }
}
