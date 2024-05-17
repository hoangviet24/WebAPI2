using ControllerAPI.Repository.Category;
using DataAnimals.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace ControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _catrgoryRepository;
        public CategoryController(ICategoryRepository catrgoryRepository)
        {
            _catrgoryRepository = catrgoryRepository;
        }
        [HttpGet("Get-All")]
        public IActionResult GetAll()
        {
            var getAll = _catrgoryRepository.GetAll();
            return Ok(getAll);
        }

        [HttpGet("Get-by-ID")]
        public IActionResult Get(int id)
        {
            var getid = _catrgoryRepository.GetById(id);
            return Ok(getid);
        }

        [HttpPost("Post")]
        public IActionResult Post([FromBody] AddCategoryDTO categoryDto)
        {
            var add = _catrgoryRepository.AddCategory(categoryDto);
            return Ok(add);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] AddCategoryDTO categoryDto, int id)
        {
            var add = _catrgoryRepository.PutCategory(categoryDto,id);
            return Ok(add);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var del = _catrgoryRepository.Delete(id);
            return Ok(del);
        }
    }
}
