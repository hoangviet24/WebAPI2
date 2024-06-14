using System.Data;
using System.Net;
using DataAnimals.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NuGet.Protocol;
using ControllerAPI.Repository.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Diagnostics;
using View.Models;
using Azure;
using DataAnimals.DTO.Login;
using DataAnimals.DTO.Animal;
using DataAnimals.Models;
using NuGet.Common;
using System.Net.Mime;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace View.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult PageLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDto)
        {
            if (ModelState.IsValid)
            {
                var clt = _httpClientFactory.CreateClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(loginRequestDto), Encoding.UTF8,
                    "application/json");
                var response = await clt.PostAsync("https://localhost:7035/api/User/Login", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Add token: {token}\n\n");
                    HttpContext.Session.SetString("Jwt", token);
                    return RedirectToAction("Index", "Animal");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Invalid login attempt: " + errorMessage);
                }
            }
            TempData["Wrong"] = "Username or Password wrong";
            return RedirectToAction("PageLogin", "Account");
        }
        [HttpPost]
        public IActionResult LogoutSuccess()
        {
            try
            {
                HttpContext.Session.Remove("Jwt");
                return RedirectToAction("PageLogin", "Account");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        public IActionResult Register()
        {
            var registerRequest = new RegisterRequestDTO();
            registerRequest.Roles = new string[] { "Read", "Write"};
            ViewBag.Roles = registerRequest.Roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterRequestDTO registerRequestDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpReq = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/User/Register"),
                    Content = new StringContent(JsonSerializer.Serialize(registerRequestDto), Encoding.UTF8,
                        MediaTypeNames.Application.Json)
                };
                var httpRes = await client.SendAsync(httpReq);

                return RedirectToAction("PageLogin", "Account");
            }
            catch
            {
                return RedirectToAction("Register", "Account");
            }

        }
    }
}
