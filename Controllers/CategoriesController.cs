using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryRequest request)
        {
            Category category = categoryRepository.Get(c => c.Name == request.Name);

            if (category == null)
            {
                Category newCategory = new Category
                {
                    Name = request.Name
                };
                categoryRepository.Add(newCategory);
                return RedirectToAction(nameof(CategoryList));
            }
             return View("Privacy","Home");
        }
        [HttpGet]
        public JsonResult GetCategoryById(int id)
        {
            var category = categoryRepository.Get(c => c.Id == id);
            if (category != null)
            {
                return Json(category);
            }
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,CreateCategoryRequest request)
        {
            Category category = categoryRepository.Get(c => c.Id == id);

            if (category != null)
            {
                category.Name = request.Name;
                categoryRepository.Update(category);
                return RedirectToAction(nameof(CategoryList));
            }
            return View("Privacy", "Home");
        }

        [HttpPost]
        public IActionResult Delete( int id) {
            Category categoryFromDb=categoryRepository.Get(c=>c.Id==id);
            
            if (categoryFromDb != null)
            {
                categoryFromDb.IsDeleted=true;
                categoryRepository.Update(categoryFromDb);
                return RedirectToAction(nameof(CategoryList));
            }
            else
            {
                return View("Privacy", "Home");
            }
        }
       
        [HttpGet]
        public IActionResult CategoryList()
        {
            List<Category> categories = categoryRepository.GetAll(c=>c.IsDeleted!=true);
            ViewBag.Categories = categories;

            return View(categories);
        }

    }
}
