using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
