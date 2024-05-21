using DataAnimals.DTO.Animal;

namespace ControllerAPI.Repository.Animal
{
    public interface IAnimalRepository
    {
        List<AnimalDto> GetAnimals();
        AnimalDto GetAnimal(int id);
        List<AnimalDto> GetbyName(string name);
        AddAnimalDto AddAnimal(AddAnimalDto animal);
        AddAnimalDto PutAnimalDto(AddAnimalDto animal, int Id);
        DataAnimals.Models.Animal Delete(int id);
    }
}
