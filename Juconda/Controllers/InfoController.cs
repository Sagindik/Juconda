using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
