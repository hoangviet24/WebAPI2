using ControllerAPI.Repository.Image;
using DataAnimals.Data;
using DataAnimals.DTO;
using DataAnimals.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public IActionResult Upload([FromForm] ImageRequestDTO dTO)
        {
            ValidateFileUpload(dTO);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = dTO.File,
                    FileExtension = Path.GetExtension(dTO.File.FileName),
                    Size = dTO.File.Length,
                    FileName = dTO.FileName,
                    FileDescription = dTO.FileDescription,
                };
                Log.Information($"Image Page => {imageDomainModel}");
                _imageRepository.upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageRequestDTO dTO)
        {
            var allowExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowExtensions.Contains(Path.GetExtension(dTO.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (dTO.File.Length > 1040000)
            {
                ModelState.AddModelError("file", "file size to big, please upload file <10M");
            }
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var getall = _imageRepository.Getlist();
            Log.Information($"Image Page => {getall}");
            return Ok(getall);
        }
        [HttpGet("GetById")]
        public IActionResult Down(int id)
        {
            var res = _imageRepository.Download(id);
            Log.Information($"Image Page => {res}");
            return File(res.Item1, res.Item2, res.Item3);
        }
        [HttpDelete("delete")]
        public IActionResult del(int id)
        {
            var del = _imageRepository.Delete(id);
            Log.Information($"Image Page => {del}");
            return Ok(del);
        }
    }
}
