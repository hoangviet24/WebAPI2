using DataAnimals.DTO.Warehouse;

namespace ControllerAPI.Repository.AnimalImage
{
    public interface IAIRepository
    {
        List<AIDto> GetAll();
        DataAnimals.Models.AnimalImage Delete(int id);
    }
}
