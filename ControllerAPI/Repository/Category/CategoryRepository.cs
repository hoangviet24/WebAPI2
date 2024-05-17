using DataAnimals.Data;
using DataAnimals.DTO;
using DataAnimals.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ControllerAPI.Repository.Category
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly DataContext _dataContext;
        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public List<CategoryDTO> GetAll()
        {
            var addCategorty = _dataContext.Categories.Select(a => new CategoryDTO()
            {
                Id = a.Id,
                Name = a.Name,
                Animal_Name = a.AnimalCategory.Select(a=>a.animal.Name).ToList()
            }).ToList();
            return addCategorty;
        }

        public CategoryDTO GetById(int id)
        {
            var getId = _dataContext.Categories.Where(a => a.Id == id);
            var getCateId = getId.Select(a => new CategoryDTO()
            {
                Id = a.Id,
                Name = a.Name,
                Animal_Name = a.AnimalCategory.Select(a => a.animal.Name).ToList()
            }).FirstOrDefault();
            return getCateId;
        }

        public AddCategoryDTO AddCategory(AddCategoryDTO categoryDTO)
        {
            var CategoryDomain = new DataAnimals.Models.Category
            {
                Name = categoryDTO.Name,
            };
            _dataContext.Categories.Add(CategoryDomain);
            _dataContext.SaveChanges();
            foreach (var item in categoryDTO.Animal_Id)
            {
                var cateAnimal = new DataAnimals.Models.AnimalCategory
                {
                    Animal_Id = item,
                    Category_Id = CategoryDomain.Id,
                };
                _dataContext.AnimalCategories.Add(cateAnimal);
                _dataContext.SaveChanges();
            }
            return categoryDTO; 
        }

        public AddCategoryDTO? PutCategory(AddCategoryDTO categoryDTO,int id)
        {
            var cateDomain = _dataContext.Categories.FirstOrDefault(a => a.Id == id);
            if (cateDomain != null)
            {
                cateDomain.Name = categoryDTO.Name;
            }

            _dataContext.Categories.Add(cateDomain);
            _dataContext.SaveChanges();
            foreach (var item in categoryDTO.Animal_Id)
            {
                var cateAnimal = new DataAnimals.Models.AnimalCategory
                {
                    Animal_Id = item,
                    Category_Id = cateDomain.Id,
                };
                _dataContext.AnimalCategories.Add(cateAnimal);
                _dataContext.SaveChanges();
            }

            return categoryDTO;
        }

        public DataAnimals.Models.Category Delete(int Id)
        {
            var getId = _dataContext.Categories.FirstOrDefault(c => c.Id == Id);
            if (getId != null)
            {
                _dataContext.Categories.Remove(getId);
                _dataContext.SaveChanges();
            }

            return getId;
        }
    }
}
