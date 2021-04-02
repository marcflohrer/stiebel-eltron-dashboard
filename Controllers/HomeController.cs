using Microsoft.AspNetCore.Mvc;

namespace StiebelEltronApiserver.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}