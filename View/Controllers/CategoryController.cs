using DataAnimals.DTO.Category;
using Microsoft.AspNetCore.Mvc;

namespace View.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            var client = _httpClientFactory.CreateClient();
            var data = await client.GetAsync("https://localhost:7035/api/Category/Get-All");
            data.EnsureSuccessStatusCode();
            categories.AddRange(await data.Content.ReadFromJsonAsync<IEnumerable<CategoryDTO>>());
            return View(categories);
        }
    }
}
