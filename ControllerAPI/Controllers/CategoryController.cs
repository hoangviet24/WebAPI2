using ControllerAPI.Repository.Category;
using DataAnimals.DTO.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using Serilog;

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
        //[Authorize(Roles = "Read")]
        [HttpGet("Get-All")]
        public IActionResult GetAll()
        {
            var getAll = _catrgoryRepository.GetAll();
            Log.Information($"Category Page => {getAll}");
            return Ok(getAll);
        }
      //  [Authorize(Roles = "Read")]
        [HttpGet("Get-by-ID")]
        public IActionResult Get(int id)
        {
            var getid = _catrgoryRepository.GetById(id); Log.Information($"Category Page => {getid}");
            return Ok(getid);
        }
        //[Authorize(Roles = "Write")]
        [HttpPost("Post")]
        public IActionResult Post([FromBody] AddCategoryDTO categoryDto)
        {
            try
            {
                var add = _catrgoryRepository.AddCategory(categoryDto);
                Log.Information($"Category Page => {categoryDto}");
                return Ok(add);
            }
            catch
            {
                return Ok("vui lòng nhập động vật");
            }
        }
      //  [Authorize(Roles = "Write")]
        [HttpPut("Update")]
        public IActionResult Update([FromBody] AddCategoryDTO categoryDto, int id)
        {
            var add = _catrgoryRepository.PutCategory(categoryDto,id);
            Log.Information($"Category Page => {categoryDto}");
            return Ok(add);
        }
      //  [Authorize(Roles = "Write")]
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var del = _catrgoryRepository.Delete(id);
                Log.Information($"Category Page => {del}");
                return Ok(del);
            }
            catch
            {
                return Ok("Kho đang chứa nó");
            }
        }
    }
}
