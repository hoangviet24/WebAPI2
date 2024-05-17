using DataAnimals.Data;
using DataAnimals.DTO;

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
                Name = a.Name,
                Description = a.Description,
                AgeAvg = a.AgeAvg
            }).ToList();
            return getall;
        }

        public AnimalDTO GetAnimal(int id)
        {
            var getId = _dataContext.Animals.Where(a => a.Id == id);
            var getDomain = getId.Select(a => new AnimalDTO
            {
                Name = a.Name,
                Description =  a.Description,
                AgeAvg = a.AgeAvg
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
                _dataContext.Animals.Add(put);
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
    }
}
