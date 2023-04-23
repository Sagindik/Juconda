using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
