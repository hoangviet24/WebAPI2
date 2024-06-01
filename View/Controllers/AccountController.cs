using System.Net;
using DataAnimals.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NuGet.Protocol;

namespace View.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

    }
}
