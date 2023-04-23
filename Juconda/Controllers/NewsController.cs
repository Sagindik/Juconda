using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
