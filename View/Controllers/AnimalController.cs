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

            if (string.IsNullOrEmpty(tokenJson))
            {
                return RedirectToAction("PageLogin", "Account");
            }

            var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
            if (tokenObj == null || !tokenObj.ContainsKey("jwtToken"))
            {
                return RedirectToAction("PageLogin", "Account");
            }
            var token = tokenObj["jwtToken"];

            Console.WriteLine("JWT Token: " + token); 
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
                if (string.IsNullOrEmpty(tokenJson))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
                if (tokenObj == null || !tokenObj.ContainsKey("jwtToken"))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var token = tokenObj["jwtToken"];
                Console.WriteLine("JWT Token: " + token); 
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
                return View("WarnUpdate");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var tokenJson = HttpContext.Session.GetString("Jwt");
                if (string.IsNullOrEmpty(tokenJson))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
                if (tokenObj == null || !tokenObj.ContainsKey("jwtToken"))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var token = tokenObj["jwtToken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                if (string.IsNullOrEmpty(tokenJson))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
                if (tokenObj == null || !tokenObj.ContainsKey("jwtToken"))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var token = tokenObj["jwtToken"];
                Console.WriteLine("JWT Token: " + token); 
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7035/api/Animal/Get-By-Id?id={Id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(requestMessage);
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
                if (string.IsNullOrEmpty(tokenJson))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
                if (tokenObj == null || !tokenObj.ContainsKey("jwtToken"))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var token = tokenObj["jwtToken"];
                Console.WriteLine("JWT Token: " + token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                var tokenJson = HttpContext.Session.GetString("Jwt");
                if (string.IsNullOrEmpty(tokenJson))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
                if (tokenObj == null || !tokenObj.ContainsKey("jwtToken"))
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                var token = tokenObj["jwtToken"];
                Console.WriteLine("JWT Token: " + token); 
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7035/api/Animal/Get-By-Id?id={Id}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(requestMessage);
                var response = await client.SendAsync(requestMessage);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("PageLogin", "Account");
                }
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Error retrieving data: " + errorMessage);
                    return View("Error"); 
                }
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
