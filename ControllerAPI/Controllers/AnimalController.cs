using Azure;
using ControllerAPI.Repository.Animal;
using DataAnimals.Data;
using DataAnimals.Models;
using DataAnimals.DTO.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace ControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository iAnimalRepository;
        private readonly DataContext _datacontext;
        public AnimalController(IAnimalRepository animal,DataContext dataContext)
        {
            iAnimalRepository = animal;
            _datacontext = dataContext;
        }
        [Authorize (Roles = "Read")]
        [HttpGet("Filter-page-sort")]
        public IActionResult Filtering(string name, int page, float pageSize,bool isAccess)
        {
            if (ModelState.IsValid)
            {
                var getid = iAnimalRepository.GetbyName(name);
                return Ok(getid);
            }
            if (ModelState.IsValid)
            {
                switch (isAccess)
                {
                    case true:
                        var post = iAnimalRepository.GetAnimals().OrderByDescending(a => a.ID).ToList();
                        return Ok(post);
                    case false:
                        var post1 = iAnimalRepository.GetAnimals().OrderBy(a => a.ID).ToList();
                        return Ok(post1);
                }
            }
            if (ModelState.IsValid)
            {
                if (_datacontext.Animals == null)
                {
                    return NotFound();
                }

                if (_datacontext.Animals.Count() == 0)
                {
                    return NoContent(); // No data found
                }

                int pageCount = (int)Math.Ceiling((decimal)(_datacontext.Animals.Count() / pageSize));

                if (page < 1 || page > pageCount)
                {
                    return BadRequest("Invalid page number"); // Handle invalid page requests
                }

                int skip = (int)((page - 1) * (int)pageSize);
                int take = (int)pageSize;

                var author = _datacontext.Animals
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                var response = new AnimalPage
                {
                    Author = author,
                    CurrentPage = (int)page,
                    Pages = pageCount,
                };

                Log.Information($"Animal Page => {response}");

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }
        [Authorize(Roles = "Read")]
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var getall = iAnimalRepository.GetAnimals();
            Log.Information($"Animal Page => {@getall}");
            return Ok(getall);
        }
        [Authorize(Roles = "Read")]
        [HttpGet("Get-By-Id")]
        public IActionResult GetId(int id)
        {
            var getId = iAnimalRepository.GetAnimal(id);
            Log.Information($"Animal Page => {@getId}");
            return Ok(getId);
        }
        [Authorize(Roles = "Write")]
        [HttpPost("Push")]
        public IActionResult Post([FromBody]AddAnimalDTO animal)
        {
            var Post = iAnimalRepository.AddAnimal(animal);
            Log.Information($"Animal Page => {animal}");
            return Ok(Post);
        }
        [Authorize(Roles = "Write")]
        [HttpPut("Update")]
        public IActionResult Put([FromBody] AddAnimalDTO animal,int id)
        {
            var Put = iAnimalRepository.PutAnimalDto(animal, id);
            Log.Information($"Animal Page => {Put}");
            return Ok(Put);
        }
        [Authorize(Roles = "Write")]
        [HttpDelete("Delete")]
        public IActionResult DeleteById(int id)
        {
            var Del = iAnimalRepository.Delete(id);
            Log.Information($"Animal Page => {Del}");
            return Ok(Del);
        }
    }
}
