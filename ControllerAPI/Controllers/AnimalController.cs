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
        //[Authorize (Roles = "Read")]
        [HttpGet("GetAll")]
        public IActionResult Filtering([FromQuery] string? name,[FromQuery] bool isAccess)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    switch (isAccess)
                    {
                        case true:
                            var post = iAnimalRepository.GetbyName(name).OrderByDescending(a => a.ID).ToList();
                            Log.Information($"Animal Page => {post}");
                            return Ok(post);
                        case false:
                            var post1 = iAnimalRepository.GetbyName(name).OrderBy(a => a.ID).ToList();
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
                        var post = iAnimalRepository.GetAnimals().OrderByDescending(a => a.ID).ToList();
                        Log.Information($"Animal Page => {post}");
                        return Ok(post);
                    case false:
                        var post1 = iAnimalRepository.GetAnimals().OrderBy(a => a.ID).ToList();
                        Log.Information($"Animal Page => {post1}");
                        return Ok(post1);
                }
            }
            
        }
        [HttpGet("Get-By-Id")]
        public IActionResult GetId(int id)
        {
            var getId = iAnimalRepository.GetAnimal(id);
            Log.Information($"Animal Page => {@getId}");
            return Ok(getId);
        }
       // [Authorize(Roles = "Write")]
        [HttpPost("Push")]
        public IActionResult Post([FromBody]AddAnimalDto animal)
        {
           
            var Post = iAnimalRepository.AddAnimal(animal);
            Log.Information($"Animal Page => {animal}");
            return Ok(Post);
        }
        //[Authorize(Roles = "Write")]
        [HttpPut("Update")]
        public IActionResult Put([FromBody] AddAnimalDto animal,int id)
        {
            var Put = iAnimalRepository.PutAnimalDto(animal, id);
            Log.Information($"Animal Page => {Put}");
            return Ok(Put);
        }
        //[Authorize(Roles = "Write")]
        [HttpDelete("Delete")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                var Del = iAnimalRepository.Delete(id);
                Log.Information($"Animal Page => {Del}");
                return Ok(Del);
            }
            catch
            {
                return Ok("Kho đang chứa nó");
            }
        }
    }
}
