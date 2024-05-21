using System.Net.Mime;
using System.Text;
using DataAnimals.DTO.Animal;
using DataAnimals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console;
using System.Text.Json;
using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Serilog;

namespace View.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AnimalController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index([FromQuery] string? filterQuery, bool isAccess)
        {
            var client = _httpClientFactory.CreateClient();
            
                List<AnimalDto> animal = new List<AnimalDto>();
                var data = await client.GetAsync("https://localhost:7035/api/Animal/GetAll?name="+filterQuery+"&isAccess="+isAccess);
                data.EnsureSuccessStatusCode();
                animal.AddRange(await data.Content.ReadFromJsonAsync<IEnumerable<AnimalDto>>());
                return View(animal);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnimal(AddAnimalDto animal)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpReq = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Animal/Push"),
                    Content = new StringContent(JsonSerializer.Serialize(animal), Encoding.UTF8,
                        MediaTypeNames.Application.Json)
                };
                var httpRes = await client.SendAsync(httpReq);
                httpRes.EnsureSuccessStatusCode();
                var data = await httpRes.Content.ReadFromJsonAsync<AddAnimalDto>();
                if (data != null)
                {
                    return RedirectToAction("Index", "Animal");
                }

                return View("Index");
            }
            catch
            {
                return View("Không có dữ liệu");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpReponse = await client.DeleteAsync("https://localhost:7035/api/Animal/Delete?id=" + id);
                httpReponse.EnsureSuccessStatusCode();
            }
            catch
            {
                return View("Info");
            }
            return RedirectToAction("Index", "Animal");
        }

        public IActionResult Info()
        {
            return View();
        }
        public async Task<IActionResult> Update(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            AnimalDto animal = new AnimalDto();
            var data = await client.GetAsync("https://localhost:7035/api/Animal/Get-By-Id?id=" + Id);
            data.EnsureSuccessStatusCode();
            animal = await data.Content.ReadFromJsonAsync<AnimalDto>();
            return View(animal);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAnimal(AddAnimalDto animal, int Id)
        {
            
            var client = _httpClientFactory.CreateClient();
            var httpReq = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("https://localhost:7035/api/Animal/Update?id=" + Id),
                Content = new StringContent(JsonSerializer.Serialize(animal), Encoding.UTF8, 
                    MediaTypeNames.Application.Json)
            }; 
            var httpRes = await client.SendAsync(httpReq);
            httpRes.EnsureSuccessStatusCode();
            var data = await httpRes.Content.ReadFromJsonAsync<AddAnimalDto>(); 
            if (data != null)
            {
                return RedirectToAction("Index", "Animal");
            }

            return View("Index");
        }
        public async Task<IActionResult> Detail(int Id)
        { 
            var client = _httpClientFactory.CreateClient();

            AnimalDto animal = new AnimalDto();
            var data = await client.GetAsync("https://localhost:7035/api/Animal/Get-By-Id?id="+Id);
            data.EnsureSuccessStatusCode();
            animal = await data.Content.ReadFromJsonAsync<AnimalDto>();
            return View(animal);
        }
        
    }
}
