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
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NuGet.Protocol.Model;
using Serilog;
using System.Net.Http.Headers;

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
            var data = await client.GetAsync("https://localhost:7035/api/Animal/GetAll?name=" + filterQuery + "&isAccess=" + isAccess);
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
                return View("Warn");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var httpReponse = await client.DeleteAsync("https://localhost:7035/api/Animal/Delete?id=" + id);
                httpReponse.EnsureSuccessStatusCode();
            }
            catch
            {
                return View("WarnDelete");
            }
            return RedirectToAction("Index", "Animal");
        }

        public IActionResult Info()
        {
            return View();
        }

        public IActionResult WarnDelete()
        {
            return View();
        }

        public IActionResult WarnUpdate()
        {
            return View();
        }
        public async Task<IActionResult> Update(int Id)
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

                // Tạo yêu cầu GET với Id
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7035/api/Animal/Get-By-Id?id={Id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(requestMessage);

                // Gửi yêu cầu GET
                AnimalDto animal = new AnimalDto();
                var data = await client.GetAsync("https://localhost:7035/api/Animal/Get-By-Id?id=" + Id);
                data.EnsureSuccessStatusCode();
                animal = await data.Content.ReadFromJsonAsync<AnimalDto>();
                return View(animal);
            }
            catch
            {
                return View("Warn");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAnimal(AddAnimalDto animal, int Id)
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

                // Tạo yêu cầu GET với Id
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7035/api/Animal/Get-By-Id?id={Id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(requestMessage);
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
            catch
            {
                return View("Warn");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            try
            {
                AnimalDto animal = new AnimalDto();
                var client = _httpClientFactory.CreateClient();

                // Lấy token từ session
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

                // Tạo yêu cầu GET với Id
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7035/api/Animal/Get-By-Id?id={Id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(requestMessage);

                // Gửi yêu cầu GET
                var response = await client.SendAsync(requestMessage);

                // Kiểm tra xem yêu cầu có thành công không
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Nếu bị từ chối truy cập, chuyển hướng đến trang đăng nhập
                    return RedirectToAction("PageLogin", "Account");
                }

                // Nếu yêu cầu không thành công với mã khác 401
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Error retrieving data: " + errorMessage);
                    return View("Error"); // Hoặc trả về một view lỗi phù hợp
                }

                // Đọc dữ liệu từ phản hồi
                animal = await response.Content.ReadFromJsonAsync<AnimalDto>();
                return View(animal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bug: " + ex);
                return View("Warn");
            }
        }
    }
}
