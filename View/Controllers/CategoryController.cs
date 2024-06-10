using DataAnimals.DTO.Animal;
using DataAnimals.DTO.Category;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace View.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        //Get all
        public async Task<IActionResult> Index([FromQuery] string? filterQuery, bool isAccess)
        {
            var client = _httpClientFactory.CreateClient();
            List<CategoryDto> category = new List<CategoryDto>();
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
            var data = await client.GetAsync($"https://localhost:7035/api/Category/GetAll?name={filterQuery}&isAccess={isAccess}");
            data.EnsureSuccessStatusCode();
            category.AddRange(await data.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>());
            return View(category);
        }
        //Add data
        public async Task<IActionResult> Create()
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
            var data = await client.GetAsync("https://localhost:7035/api/Animal/GetAll");
            data.EnsureSuccessStatusCode();
            animal.AddRange(await data.Content.ReadFromJsonAsync<IEnumerable<AnimalDto>>());
            ViewBag.ListAnimal = animal;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(AddCategoryDto category)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
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
                var httpReq = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Category/Post"),
                    Content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8,
                        MediaTypeNames.Application.Json)
                };
                var httpRes = await client.SendAsync(httpReq);
                httpRes.EnsureSuccessStatusCode();
                var data = await httpRes.Content.ReadFromJsonAsync<AddCategoryDto>();
                if (data != null)
                {
                    return RedirectToAction("Index", "Category");
                }

                return View("Index");
            }
            catch
            {
                return View("Warn");
            }
        }
        //Delete
        [HttpGet]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
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
                var httpReponse = await client.DeleteAsync("https://localhost:7035/api/Category/Delete?id=" + id);
                httpReponse.EnsureSuccessStatusCode();
            }
            catch
            {
                return View("WarnDelete");

            }
            return RedirectToAction("Index", "Category");
        }
        //Update
        public async Task<IActionResult> UpdateCtx(int Id)
        {
            var client = _httpClientFactory.CreateClient();
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
            CategoryDto category = new CategoryDto();
            var data = await client.GetAsync("https://localhost:7035/api/Category/Get-by-ID?id=" + Id);
            data.EnsureSuccessStatusCode();
            category = await data.Content.ReadFromJsonAsync<CategoryDto>();
            ViewBag.Id = category;
            //list animal
            List<AnimalDto> animal = new List<AnimalDto>();
            var dataAnimal = await client.GetAsync("https://localhost:7035/api/Animal/GetAll");
            dataAnimal.EnsureSuccessStatusCode();
            animal.AddRange(await dataAnimal.Content.ReadFromJsonAsync<IEnumerable<AnimalDto>>());
            ViewBag.ListAnimal = animal;
            AddCategoryDto addCategoryDto = new AddCategoryDto();
            return View(addCategoryDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(AddCategoryDto category, int Id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
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
                var httpReq = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("https://localhost:7035/api/Category/Update?id=" + Id),
                    Content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8,
                        MediaTypeNames.Application.Json)
                };
                var httpRes = await client.SendAsync(httpReq);
                httpRes.EnsureSuccessStatusCode();
                var data = await httpRes.Content.ReadFromJsonAsync<AddCategoryDto>();
                if (data != null)
                {
                    return RedirectToAction("Index", "Category");
                }
                return RedirectToAction("Index", "Category");
            }
            catch
            {
                return View("WarnUpdate");
            }
            
        }
    }
}
