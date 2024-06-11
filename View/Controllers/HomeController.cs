using System.Drawing.Printing;
using DataAnimals.DTO.Animal;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace View.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var client = _httpClientFactory.CreateClient();
            List<AnimalDto> animal = new List<AnimalDto>();
            var tokenJson = HttpContext.Session.GetString("Jwt");

            // Kiểm tra xem token có tồn tại không
            if (string.IsNullOrEmpty(tokenJson))
            {
                // Nếu không tồn tại, chuyển hướng đến trang đăng nhập
                return RedirectToAction("PageLogin", "Account");
            }

            // Giải mã JSON để lấy giá trị của token
            var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
            if (tokenObj == null || !tokenObj.ContainsKey("jwtToken"))
            {
                return RedirectToAction("PageLogin", "Account");
            }
            var token = tokenObj["jwtToken"];

            // Thêm token vào header Authorization của yêu cầu HTTP
            Console.WriteLine("JWT Token: " + token); // Ghi log token để kiểm tra
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            {
                List<AnimalDto> animals = new List<AnimalDto>();
                var dataAnimal = await client.GetAsync("https://localhost:7035/api/Animal/GetAll");
                dataAnimal.EnsureSuccessStatusCode();
                animals.AddRange(await dataAnimal.Content.ReadFromJsonAsync<IEnumerable<AnimalDto>>());
                ViewBag.ListAnimal = animals;
                ViewBag.CurrentPage = page;
            }

            var data = await client.GetAsync($"https://localhost:7035/api/Animal/Get-Page?page={page}&pagesize=5");
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
