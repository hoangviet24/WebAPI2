using DataAnimals.DTO.Warehouse;
using DataAnimals.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

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
            try
            {
                List<ACDto> animals = new List<ACDto>();
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
                var data = await client.GetAsync("https://localhost:7035/api/AC/Get-All");
                data.EnsureSuccessStatusCode();
                animals.AddRange(await data.Content.ReadFromJsonAsync<IEnumerable<ACDto>>());
                return View(animals);
            }
            catch
            {
                return View("WarnGetUser");
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
                Console.WriteLine("JWT Token: " + token); 
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var httpReponse = await client.DeleteAsync("https://localhost:7035/api/AC/Delete?Id=" + id);
                httpReponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "WareHouse");
            }
            catch
            {
                return View("WarnDelete");
            }
        }
    }
}
