using ControllerAPI.Repository.Animal;
using DataAnimals.Data;
using DataAnimals.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository iAnimalRepository;
        public AnimalController(IAnimalRepository animal)
        {
            iAnimalRepository = animal;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var getall = iAnimalRepository.GetAnimals();
            return Ok(getall);
        }

        [HttpGet("Get-By-Id")]
        public IActionResult GetId(int id)
        {
            var getId = iAnimalRepository.GetAnimal(id);
            return Ok(getId);
        }

        [HttpPost("Push")]
        public IActionResult Post([FromBody]AddAnimalDTO animal)
        {
            var Post = iAnimalRepository.AddAnimal(animal);
            return Ok(Post);
        }

        [HttpPut("Update")]
        public IActionResult Put([FromBody] AddAnimalDTO animal,int id)
        {
            var Put = iAnimalRepository.PutAnimalDto(animal, id);
            return Ok(Put);
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteById(int id)
        {
            var Del = iAnimalRepository.Delete(id);
            return Ok(Del);
        }
    }
}
