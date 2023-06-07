using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
