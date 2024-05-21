using DataAnimals.DTO.Warehouse;
using DataAnimals.Models;
using Microsoft.AspNetCore.Mvc;

namespace View.Controllers
{
    public class WareHouseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WareHouseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ACDto> animals = new List<ACDto>();
            var client = _httpClientFactory.CreateClient();
            var data = await client.GetAsync("https://localhost:7035/api/AC/Get-All");
            data.EnsureSuccessStatusCode();
            animals.AddRange(await data.Content.ReadFromJsonAsync<IEnumerable<ACDto>>());
            return View(animals);
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var client = _httpClientFactory.CreateClient();
            var httpReponse = await client.DeleteAsync("https://localhost:7035/api/AC/Delete?Id=" + id);
            httpReponse.EnsureSuccessStatusCode();
            return RedirectToAction("Index", "WareHouse");
        }
    }
}
