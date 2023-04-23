using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
