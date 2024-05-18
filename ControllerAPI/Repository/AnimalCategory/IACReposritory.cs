using DataAnimals.DTO.Warehouse;

namespace ControllerAPI.Repository.AnimalCategory
{
    public interface IACReposritory
    {
        List<ACDto> GetRepos();
        DataAnimals.Models.AnimalCategory Delete(int ID);
    }
}
