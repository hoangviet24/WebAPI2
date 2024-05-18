using DataAnimals.Models;

namespace ControllerAPI.Controllers
{
    public class AnimalPage
    {
        public List<Animal> Author { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}