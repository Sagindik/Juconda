using AutoMapper;
using Juconda.Infrastructure;
using Juconda.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Juconda.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public HomeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var bestProducts = _context.Products.Where(_ => _.Actual && _.IsBestseller).ToList();

            var models = _mapper.Map<List<ProductViewModel>>(bestProducts);

            return View("Index", models);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}