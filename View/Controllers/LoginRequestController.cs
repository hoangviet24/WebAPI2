using Microsoft.AspNetCore.Mvc;

namespace View.Controllers
{
    public class LoginRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }
    }
}
