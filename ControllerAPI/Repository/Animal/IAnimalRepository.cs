using DataAnimals.DTO.Animal;

namespace ControllerAPI.Repository.Animal
{
    public interface IAnimalRepository
    {
        List<AnimalDTO> GetAnimals();
        AnimalDTO GetAnimal(int id);
        AnimalDTO GetbyName(string name);
        AddAnimalDTO AddAnimal(AddAnimalDTO animal);
        AddAnimalDTO PutAnimalDto(AddAnimalDTO animal, int Id);
        DataAnimals.Models.Animal Delete(int id);
    }
}
