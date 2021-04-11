using Microsoft.AspNetCore.Mvc;

namespace StiebelEltronApiServer.Controllers
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