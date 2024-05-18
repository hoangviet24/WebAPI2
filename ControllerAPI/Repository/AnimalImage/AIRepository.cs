using DataAnimals.Data;
using DataAnimals.DTO.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI.Repository.AnimalImage
{
    public class AIRepository : IAIRepository
    {
        private readonly DataContext _dataContext;
        public AIRepository(DataContext dataContext)
        {  _dataContext = dataContext; }
        public List<AIDto> GetAll()
        {
            var getall = _dataContext.AnimalImages.Select(ac => new AIDto()
            {
                Id = ac.Id,
                Animal_Name = ac.Animal.Name,
                Image_Name = ac.Image.FileName,
            }).ToList();
            return getall;
        }
        public DataAnimals.Models.AnimalImage Delete(int id)
        {
            var getId = _dataContext.AnimalImages.FirstOrDefault(ac => ac.Id == id);
            if (getId != null)
            {
                _dataContext.AnimalImages.Remove(getId);
                _dataContext.SaveChanges();
            }

            return getId;
        }
    }
}
