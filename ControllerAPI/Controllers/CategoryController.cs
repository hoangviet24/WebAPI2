using ControllerAPI.Repository.Animal;
using ControllerAPI.Repository.Category;
using DataAnimals.Data;
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
        private readonly DataContext _datacontext;
        public CategoryController(ICategoryRepository catrgoryRepository, DataContext dataContext)
        {
            _datacontext = dataContext;
            _catrgoryRepository = catrgoryRepository;
        }
        [Authorize(Roles = "Read")]
        [HttpGet("GetAll")]
        public IActionResult Filtering([FromQuery] string? name, [FromQuery] bool isAccess)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    switch (isAccess)
                    {
                        case true:
                            var post = _catrgoryRepository.GetbyName(name).OrderByDescending(a => a.Id).ToList();
                            Log.Information($"Animal Page => {post}");
                            return Ok(post);
                        case false:
                            var post1 = _catrgoryRepository.GetbyName(name).OrderBy(a => a.Id).ToList();
                            Log.Information($"Animal Page => {post1}");
                            return Ok(post1);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                switch (isAccess)
                {
                    case true:
                        var post = _catrgoryRepository.GetAll().OrderByDescending(a => a.Id).ToList();
                        Log.Information($"Animal Page => {post}");
                        return Ok(post);
                    case false:
                        var post1 = _catrgoryRepository.GetAll().OrderBy(a => a.Id).ToList();
                        Log.Information($"Animal Page => {post1}");
                        return Ok(post1);
                }
            }
        }
        [Authorize(Roles = "Read")]
        [HttpGet("Get-by-ID")]
        public IActionResult Get(int id)
        {
            var getid = _catrgoryRepository.GetById(id); Log.Information($"Category Page => {getid}");
            return Ok(getid);
        }
        //[Authorize(Roles = "Write")]
        [HttpPost("Post")]
        public IActionResult Post([FromBody] AddCategoryDto categoryDto)
        {
            var add = _catrgoryRepository.AddCategory(categoryDto);
            Log.Information($"Category Page => {categoryDto}");
            return Ok(add);
        }
        [Authorize(Roles = "Write")]
        [HttpPut("Update")]
        public IActionResult Update([FromBody] AddCategoryDto categoryDto, int id)
        {
            var add = _catrgoryRepository.PutCategory(categoryDto,id);
            Log.Information($"Category Page => {categoryDto}");
            return Ok(add);
        }
        [Authorize(Roles = "Write")]
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
