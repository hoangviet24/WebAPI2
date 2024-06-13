using DataAnimals.Data;
using DataAnimals.DTO.Animal;
using DataAnimals.DTO.Category;
using System.Linq;

namespace ControllerAPI.Repository.Animal
{
    public class AnimalRepository:IAnimalRepository
    {
        private readonly DataContext _dataContext;

        // thiết lập constructor với Data
        public AnimalRepository(DataContext dataContext)
        {
            _dataContext= dataContext;
        }
        // Lấy tất cả
        public List<AnimalDto> GetAnimals()
        {
            // dùng phương thức Select để lấy dữ liệu dto từ dữ liệu chính
            var getall = _dataContext.Animals.Select(a => new AnimalDto()
            {
                ID = a.Id,
                Name = a.Name,
                Description = a.Description,
                AgeAvg = a.AgeAvg,
                Url = a.Url,
                Loai = a.AnimalCategory.Select(ac=>ac.category.Name).FirstOrDefault(),
            }).ToList();
            return getall;
        }

        public AnimalDto GetAnimal(int id)
        {
            // như ở trên nhưng sẽ dùng where để lấy dữ liệu
            var getId = _dataContext.Animals.Where(a => a.Id == id);
            var getDomain = getId.Select(a => new AnimalDto
            {
                ID = a.Id,
                Name = a.Name,
                Description =  a.Description,
                AgeAvg = a.AgeAvg,
                Url =a.Url,
                Loai = a.AnimalCategory.Select(ac => ac.category.Name).FirstOrDefault(),
            }).FirstOrDefault();
            return getDomain;
        }

        public AddAnimalDto AddAnimal(AddAnimalDto animal)
        {
            // truyền dữ liệu dto vào data
            var AnimalDomain = new DataAnimals.Models.Animal()
            {
                Name = animal.Name,
                Description = animal.Description,
                AgeAvg = (float)animal.AgeAvg,
                Url = animal.Url
            };
            _dataContext.Animals.Add(AnimalDomain);
            _dataContext.SaveChanges();
            
            return animal;
        }

        public AddAnimalDto PutAnimalDto(AddAnimalDto animal, int Id)
        {
            // Lấy ID để thay đổi dữ liệu
            var put = _dataContext.Animals.FirstOrDefault(a => a.Id == Id);
            if (put != null)
            {
                put.Name = animal.Name;
                put.Description = animal.Description;
                put.AgeAvg = animal.AgeAvg;
                put.Url = animal.Url;
                _dataContext.SaveChanges();
            }
            // sau đó vô kho chứa để xóa tất cả dữ liệu
            var putDomain = _dataContext.AnimalCategories.Where(a => a.Id == Id);
            if (putDomain != null)
            {
                _dataContext.AnimalCategories.RemoveRange(putDomain);
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

        public List<AnimalDto> GetbyName(string name)
        {
            var getId = _dataContext.Animals.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            var getDomain = getId.Select(a => new AnimalDto()
            {
                ID = a.Id,
                Name = a.Name,
                Description = a.Description,
                AgeAvg = a.AgeAvg,
                Url = a.Url
            }).ToList();
            return getDomain;
        }
    }
}
