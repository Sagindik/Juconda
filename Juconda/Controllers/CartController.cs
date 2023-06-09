using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
