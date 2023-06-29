using Juconda.Areas.admin.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Juconda.Areas.admin.Controllers
{
	[Area("admin")]
	[CustomAuthorize]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
