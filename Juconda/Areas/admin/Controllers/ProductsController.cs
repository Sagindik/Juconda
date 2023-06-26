using AutoMapper;
using Juconda.Areas.admin.Attributes;
using Juconda.Areas.admin.ViewModels.Categories;
using Juconda.Areas.admin.ViewModels.Pagination;
using Juconda.Areas.admin.ViewModels.Products;
using Juconda.Domain.Models;
using Juconda.Framework.Queryable.FilterExtension;
using Juconda.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace Juconda.Areas.admin.Controllers
{
    [Area("admin")]
    [CustomAuthorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index(ProductRequest request)
        {
            return View(GetProductsBasePage(request));
        }

        public BasePage<ProductModel> GetProductsBasePage(ProductRequest request)
        {
            var products = _context.Products.Where(_ => _.Actual).AsQueryable();

            if (!request.NameContains.IsNullOrEmpty())
                products = products.Where(_ => !string.IsNullOrEmpty(_.Name) && _.Name.ToLower().Contains(request.NameContains.ToLower()));

            var models = _mapper.Map<List<ProductModel>>(products.ToList());

            var result = new BasePage<ProductModel>
            {
                Objects = models.ExecutePageFilter(request.Page, request.Take),
                Pagination = new PageViewModel(request, models.Count)
            };

            return result;
        }

        [HttpPost]
        public IActionResult GetProductsPage(ProductRequest request)
        {
            var model = GetProductsBasePage(request);
            return PartialView("Products", model);
        }

        public IActionResult Create()
        {
            var categories = _context.Categories.Where(_ => _.Actual).ToList();
            ViewData["Categories"] = new SelectList(categories.Select(_ => new { _.Id, _.Name }), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            var categories = _context.Categories.Where(_ => _.Actual).ToList();
            ViewData["Categories"] = new SelectList(categories.Select(_ => new { _.Id, _.Name }), "Id", "Name");

            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Product>(model);

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    string contentType = model.ImageFile.ContentType;

                    // Проверка MIME-типа файла
                    if (contentType != "image/jpeg" && contentType != "image/png" && contentType != "image/jpg")
                    {
                        ModelState.AddModelError("ImageFile", "Only JPG, JPEG and PNG files are allowed.");
                        return View(model);
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        model.ImageFile.CopyTo(memoryStream);
                        entity.Image = memoryStream.ToArray();
                    }
                }

                _context.Products.Add(entity);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
