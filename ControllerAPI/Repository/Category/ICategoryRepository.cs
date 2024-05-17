
using DataAnimals.DTO;

namespace ControllerAPI.Repository.Category
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAll();
        CategoryDTO GetById(int id);
        AddCategoryDTO AddCategory(AddCategoryDTO categoryDTO);
        AddCategoryDTO? PutCategory(AddCategoryDTO categoryDTO,int id);
        DataAnimals.Models.Category Delete(int Id);
    }
}
