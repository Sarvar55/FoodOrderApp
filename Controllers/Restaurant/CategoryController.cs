using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Services.Transaction;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Controllers.Restaurant
{
    [Authorize(Roles =Constants.COMPANY)]
    [Route("Restaurant")]
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository categoryRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IDishRepository  dishRepository;
        private readonly TransactionService transactionService;

        public CategoryController(ICategoryRepository categoryRepository, IDishRepository dishRepository, ICompanyRepository companyRepository, TransactionService transactionService)
        {
            this.categoryRepository = categoryRepository;
            this.companyRepository = companyRepository;
            this.transactionService = transactionService;
            this.dishRepository= dishRepository;
        }

        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> Index()
        {
            List<Category> categoires = categoryRepository.GetCategoriesAndDishes();
            List<Company> companies = companyRepository.GetAll(c=>c.IsDeleted!=true);
            ViewBag.Companies = companies;
            return View("~/Views/Company/Category/Index.cshtml", categoires);
        }

        [HttpPost]
        [Route("Category/Create")]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var model = new Category()
                {
                    CompanyId = request.CompanyId,
                    Name = request.Name,
                };

                categoryRepository.Add(model);
                return Json(model);
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
            return Json(null);
        }
        [HttpPost]
        [Route("Category/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           
            Category category = categoryRepository.Get(c=>c.Id== id);  

            if (category != null)
            {
                try
                {
                   category.IsDeleted = true;
                    categoryRepository.Update(category);
                    return Json(category.Name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return Json(null);
        }

        [HttpGet]
        [Route("Category/{categoryId}/Meals")]

        public async Task<IActionResult> GettAllProductsByCategoryId(int categoryId)
        {
          
            ViewData["Category"] = categoryRepository.Get(c => c.Id == categoryId);
            List<Dish> dishes = dishRepository.GetDishesByCategoryId(categoryId);
            return View("~/Views/Company/Category/Meal/Index.cshtml", dishes);
        }

    }
}
