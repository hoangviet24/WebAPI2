using ControllerAPI.Repository.AnimalImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IAIRepository _aiRepository;
        public AIController(IAIRepository aiRepository)
        {
            _aiRepository = aiRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var get = _aiRepository.GetAll();
            return Ok(get);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var getid = _aiRepository.Delete(id);
            return Ok(getid);
        }
    }
}
