using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
