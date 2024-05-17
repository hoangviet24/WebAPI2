using ControllerAPI.Repository.AnimalCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ACController : ControllerBase
    {
        private readonly IACReposritory reposritory;

        public ACController(IACReposritory reposritory)
        {
            this.reposritory = reposritory;
        }

        [HttpGet("Get-All")]
        public ActionResult Get()
        {
            var getall = reposritory.GetRepos();
            return Ok(getall);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int Id)
        {
            var getid = reposritory.Delete(Id);
            return Ok(getid);
        }
    }
}
