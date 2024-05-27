using DataAnimals.DTO.Animal;
using Microsoft.AspNetCore.Mvc;

namespace View.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            List<AnimalDto> animal = new List<AnimalDto>();
            var data = await client.GetAsync("https://localhost:7035/api/Animal/GetAll");
            data.EnsureSuccessStatusCode();
            animal.AddRange(await data.Content.ReadFromJsonAsync<IEnumerable<AnimalDto>>());
            return View(animal);
        }
    }
    public static class StringHelpers
    {
        public static string TruncateAtWord(string input, int wordLimit)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length <= wordLimit)
                return input;

            return string.Join(" ", words.Take(wordLimit)) + "...";
        }
    }
}
