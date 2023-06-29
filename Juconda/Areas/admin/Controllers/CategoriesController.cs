using AutoMapper;
using Juconda.Areas.admin.Attributes;
using Juconda.Areas.admin.ViewModels.Categories;
using Juconda.Areas.admin.ViewModels.Pagination;
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
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index(CategoryRequest request)
        {
            return View(GetCategoriesBasePage(request));
        }

        public BasePage<CategoryModel> GetCategoriesBasePage(CategoryRequest request)
        {
            var categories = _context.Categories.Where(_ => _.Actual).AsQueryable();

            if (!request.NameContains.IsNullOrEmpty())
                categories = categories.Where(_ => !string.IsNullOrEmpty(_.Name) && _.Name.ToLower().Contains(request.NameContains.ToLower()));

            var models = _mapper.Map<List<CategoryModel>>(categories.ToList());

            var result = new BasePage<CategoryModel>
            {
                Objects = models.ExecutePageFilter(request.Page, request.Take),
                Pagination = new PageViewModel(request, models.Count)
            };

            return result;
        }

        [HttpPost]
        public IActionResult GetCategoriesPage(CategoryRequest request)
        {
            var model = GetCategoriesBasePage(request);
            return PartialView("Categories", model);
        }

        public IActionResult Create()
        {
            var categories = _context.Categories.Where(_ => _.Actual).ToList();
            ViewData["Categories"] = new SelectList(categories.Select(_ => new { _.Id, _.Name }), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            var categories = _context.Categories.Where(_ => _.Actual).ToList();
            ViewData["Categories"] = new SelectList(categories.Select(_ => new { _.Id, _.Name }), "Id", "Name");

            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Category>(model);

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

                _context.Categories.Add(entity);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
