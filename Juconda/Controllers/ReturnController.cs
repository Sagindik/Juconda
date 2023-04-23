using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class ReturnController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
