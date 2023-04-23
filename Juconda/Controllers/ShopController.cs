using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
