using AutoMapper;
using Juconda.Infrastructure;
using Juconda.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Details(int productId)
        {
            var product = _context.Products.FirstOrDefault(_ => _.Id == productId);

            if (product == null) return NotFound();

            var model = _mapper.Map<ProductViewModel>(product);

            return View(model);
        }
    }
}
