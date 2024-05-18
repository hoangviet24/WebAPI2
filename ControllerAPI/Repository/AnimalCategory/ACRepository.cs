using DataAnimals.Data;
using DataAnimals.DTO.Warehouse;

namespace ControllerAPI.Repository.AnimalCategory
{
    public class ACRepository:IACReposritory
    {
        private readonly DataContext _context;

        public ACRepository(DataContext context)
        {
            _context = context;
        }
        public List<ACDto> GetRepos()
        {
            var getall = _context.AnimalCategories.Select(ac => new ACDto
            {
                Id = ac.Id,
                Animal_Name = ac.animal.Name,
                Category_Name = ac.category.Name,
            }).ToList();
            return getall;
        }

        public DataAnimals.Models.AnimalCategory Delete(int ID)
        {
            var getId = _context.AnimalCategories.FirstOrDefault(ac => ac.Id == ID);
            if (getId != null)
            {
                _context.AnimalCategories.Remove(getId);
                _context.SaveChanges();
            }

            return getId;
        }
    }
}
