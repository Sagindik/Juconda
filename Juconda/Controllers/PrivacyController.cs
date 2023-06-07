using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
