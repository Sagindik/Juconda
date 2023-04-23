using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
