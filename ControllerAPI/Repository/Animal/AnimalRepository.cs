using DataAnimals.Data;
using DataAnimals.DTO.Animal;
using DataAnimals.DTO.Category;

namespace ControllerAPI.Repository.Animal
{
    public class AnimalRepository:IAnimalRepository
    {
        private readonly DataContext _dataContext;

        public AnimalRepository(DataContext dataContext)
        {
            _dataContext= dataContext;
        }
        public List<AnimalDTO> GetAnimals()
        {
            var getall = _dataContext.Animals.Select(a => new AnimalDTO()
            {
                ID = a.Id,
                Name = a.Name,
                Description = a.Description,
                AgeAvg = a.AgeAvg,
                filename = a.AnimalImage.Select(b => b.Image.FileName).ToList()
            }).ToList();
            return getall;
        }

        public AnimalDTO GetAnimal(int id)
        {
            var getId = _dataContext.Animals.Where(a => a.Id == id);
            var getDomain = getId.Select(a => new AnimalDTO
            {
                ID = a.Id,
                Name = a.Name,
                Description =  a.Description,
                AgeAvg = a.AgeAvg,
                filename = a.AnimalImage.Select(b => b.Image.FileName).ToList()
            }).FirstOrDefault();
            return getDomain;
        }

        public AddAnimalDTO AddAnimal(AddAnimalDTO animal)
        {
            var AnimalDomain = new DataAnimals.Models.Animal()
            {
                Name = animal.Name,
                Description = animal.Description,
                AgeAvg = (float)animal.AgeAvg,
            };
            foreach (var item in animal.file_Id)
            {
                var cateAnimal = new DataAnimals.Models.AnimalImage()
                {
                    Animal_Id = AnimalDomain.Id,
                    Image_Id = item
                };
                _dataContext.AnimalImages.Add(cateAnimal);
                _dataContext.SaveChanges();
            }
            _dataContext.Animals.Add(AnimalDomain);
            _dataContext.SaveChanges();
            return animal;
        }

        public AddAnimalDTO PutAnimalDto(AddAnimalDTO animal, int Id)
        {
            var put = _dataContext.Animals.FirstOrDefault(a => a.Id == Id);
            if (put != null)
            {
                put.Name = animal.Name;
                put.Description = animal.Description;
                put.AgeAvg = (float)animal.AgeAvg;
                _dataContext.SaveChanges();
            }

            var putDomain = _dataContext.AnimalCategories.Where(a => a.Id == Id);
            if (putDomain != null)
            {
                _dataContext.AnimalCategories.RemoveRange(putDomain);
                _dataContext.SaveChanges();
            }
            foreach (var item in animal.file_Id)
            {
                var cateAnimal = new DataAnimals.Models.AnimalImage()
                {
                    Animal_Id = Id,
                    Image_Id = item
                };
                _dataContext.AnimalImages.Add(cateAnimal);
                _dataContext.SaveChanges();
            }
            return animal;
        }

        public DataAnimals.Models.Animal Delete(int id)
        {
            var getId = _dataContext.Animals.FirstOrDefault(a=>a.Id == id);
            if (getId != null)
            {
                _dataContext.Animals.Remove(getId);
                _dataContext.SaveChanges();
            }

            return getId;
        }

        public AnimalDTO GetbyName(string? name)
        {
            var getId = _dataContext.Animals.Where(a => a.Name.Contains(name));
            var getDomain = getId.Select(a => new AnimalDTO
            {
                ID = a.Id,
                Name = a.Name,
                Description = a.Description,
                AgeAvg = a.AgeAvg,
                filename = a.AnimalImage.Select(b => b.Image.FileName).ToList()
            }).FirstOrDefault();
            return getDomain;
        }
    }
}
