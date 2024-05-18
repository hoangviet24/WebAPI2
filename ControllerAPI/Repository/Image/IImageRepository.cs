using DataAnimals.Models;
using DataAnimals.Models;
namespace ControllerAPI.Repository.Image
{
    public interface IImageRepository
    {
        DataAnimals.Models.Image upload(DataAnimals.Models.Image image);
        List<DataAnimals.Models.Image> Getlist();
        (byte[], string, string) Download(int id);
        public DataAnimals.Models.Image? Delete(int id);
    }
}
