using DataAnimals.DTO.Animal;
using DataAnimals.DTO.Category;

namespace ControllerAPI.Repository.Category
{
    public interface ICategoryRepository
    {
        List<CategoryDto> GetAll();
        CategoryDto GetById(int id);
        List<CategoryDto> GetbyName(string name);
        AddCategoryDto AddCategory(AddCategoryDto categoryDTO);
        AddCategoryDto? PutCategory(AddCategoryDto categoryDTO,int id);
        DataAnimals.Models.Category Delete(int Id);

    }
}
