using System.Reflection.Metadata.Ecma335;
using ControllerAPI.Repository.AnimalCategory;
using DataAnimals.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
        [Authorize(Roles = "Write")]
        [HttpGet("Get-All")]
        public ActionResult Get()
        {
            var getall = reposritory.GetRepos();
            return Ok(getall);
        }
        [Authorize(Roles = "Write")]
        [HttpDelete("Delete")]
        public ActionResult Delete(int Id)
        {
            var getid = reposritory.Delete(Id);
            return Ok(getid);
        }
    }
}
