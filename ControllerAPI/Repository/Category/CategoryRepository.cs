using DataAnimals.Data;
using DataAnimals.DTO.Animal;
using DataAnimals.DTO.Category;
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
        public List<CategoryDto> GetAll()
        {
            var addCategorty = _dataContext.Categories.Select(a => new CategoryDto()
            {
                Id = a.Id,
                Name = a.Name,
                Animal_Name = a.AnimalCategory.Select(a=>a.animal.Name).ToList()
            }).ToList();
            return addCategorty;
        }

        public CategoryDto GetById(int id)
        {
            var getId = _dataContext.Categories.Where(a => a.Id == id);
            var getCateId = getId.Select(a => new CategoryDto()
            {
                Id = a.Id,
                Name = a.Name,
                Animal_Name = a.AnimalCategory.Select(a => a.animal.Name).ToList()
            }).FirstOrDefault();
            return getCateId;
        }

        public AddCategoryDto AddCategory(AddCategoryDto categoryDTO)
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

        public AddCategoryDto? PutCategory(AddCategoryDto categoryDTO,int id)
        {
            var cateDomain = _dataContext.Categories.FirstOrDefault(a => a.Id == id);
            if (cateDomain != null)
            {
                cateDomain.Name = categoryDTO.Name;
                _dataContext.SaveChanges();
            }

            var animalDomail = _dataContext.AnimalCategories.Where(a => a.Id == id);
            if (animalDomail != null)
            {
                _dataContext.AnimalCategories.RemoveRange(animalDomail);
                _dataContext.SaveChanges();
            }
        
            foreach (var item in categoryDTO.Animal_Id)
            {
                var cateAnimal = new DataAnimals.Models.AnimalCategory
                {
                    Animal_Id = item,
                    Category_Id = id,
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
        public List<CategoryDto> GetbyName(string name)
        {
            var getId = _dataContext.Categories.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            var getDomain = getId.Select(a => new CategoryDto()
            {
                Id = a.Id,
                Name = a.Name,
                Animal_Name = a.AnimalCategory.Select(a => a.animal.Name).ToList()
            }).ToList();
            return getDomain;
        }
    }
}
