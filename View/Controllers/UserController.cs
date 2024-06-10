using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Web;
using DataAnimals.DTO.Login;
using Microsoft.AspNetCore.Mvc;
using View.Models;

namespace View.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult WarnGetUser()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<UserDTO> roles = new List<UserDTO>();
                var clt = _httpClientFactory.CreateClient();
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
                clt.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var data = await clt.GetAsync("https://localhost:7035/api/User/ListUsers");
                data.EnsureSuccessStatusCode();
                roles.AddRange(await data.Content.ReadFromJsonAsync<IEnumerable<UserDTO>>());
                return View(roles);
            }
            catch (Exception e)
            {
                return View("WarnGetUser");
            }
        }

        public async Task<IActionResult> DetailUser(string username)
        {
            UserDTO user = new UserDTO();
            var clt = _httpClientFactory.CreateClient();
            var data = await clt.GetAsync($"https://localhost:7035/api/User/GetUser/{username}");
            data.EnsureSuccessStatusCode();
            user = await data.Content.ReadFromJsonAsync<UserDTO>();
            return View(user);
        }

        public async Task<IActionResult> Update(string username)
        {
            {
                UserDTO user = new UserDTO();
                var clt = _httpClientFactory.CreateClient();
                var data = await clt.GetAsync($"https://localhost:7035/api/User/GetUser/{username}");
                data.EnsureSuccessStatusCode();
                user = await data.Content.ReadFromJsonAsync<UserDTO>();
                ViewBag.User = user;
            }
            var registerRequest = new RegisterRequestDTO();
            registerRequest.Roles = new string[] { "Read", "Write" };
            ViewBag.Roles = registerRequest.Roles;
            return View(); ;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserView([FromQuery] UpdateUserRolesDTO update,string username)
        {
            var clt = _httpClientFactory.CreateClient();
            var httpReq = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7035/api/User/UpdateRoles?username={username}"),
                Content = new StringContent(JsonSerializer.Serialize(update), Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };
            var data = await clt.SendAsync(httpReq);
            data.EnsureSuccessStatusCode();
            var roles = await data.Content.ReadFromJsonAsync<UpdateUserRolesDTO>();
            if (roles != null)
            {
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
            return View("Error", new ErrorViewModel { ErrorMessage = "An unexpected error occurred. Please try again later." });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUserView(string username)
        {
            try
            {
                var clt = _httpClientFactory.CreateClient();
                var data = await clt.DeleteAsync($"https://localhost:7035/api/User/DeleteUser/{username}");
                data.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "User");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}