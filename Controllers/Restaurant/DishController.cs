
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.Concrete;
using FoodOrderApp.Security;
using FoodOrderApp.Services.Transaction;
using FoodOrderApp.Utility;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FoodOrderApp.Controllers.Restaurant
{


   
    [Route("Restaurant")]
    public class DishController : Controller
    {
        private readonly IDishRepository dishRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly FileUtility fileUtility;
        private readonly TransactionService transactionService;
        private readonly IRatingRepository ratingRepository;
        private readonly PrincipalHelper principal;

        public DishController(IDishRepository dishRepository, ICategoryRepository categoryRepository, ICompanyRepository companyRepository, FileUtility fileUtility, TransactionService transactionService, IRatingRepository ratingRepository, PrincipalHelper principal)
        {
            this.dishRepository = dishRepository;
            this.categoryRepository = categoryRepository;
            this.companyRepository = companyRepository;
            this.fileUtility = fileUtility;
            this.transactionService = transactionService;
            this.ratingRepository = ratingRepository;
            this.principal = principal;
        }

        [HttpGet]
        [Route("Meals")]
        public  IActionResult Index()
        {
           return View("~/Views/Company/Meal/Index.cshtml", dishRepository.GetDishesWithCategoryAndCompany());
            
        }

        [HttpGet]
        [Authorize(Roles = $"{Constants.ADMIN},{Constants.COMPANY}")]
        [Route("Meal/Create")]
        public async Task<IActionResult> Create()
        {
            CompanyCategory companyCategory=new CompanyCategory();
            companyCategory.Companys = companyRepository.GetAll(c => c.IsDeleted != true);
            companyCategory.Categories = categoryRepository.GetAll(c => c.IsDeleted != true);
            return View("~/Views/Company/Meal/Create.cshtml", companyCategory);
        }

        [HttpPost]
        [Route("Meal/Create")]
        [Authorize(Roles = $"{Constants.ADMIN},{Constants.COMPANY}")]
        public async Task<IActionResult> Create(CreateDishRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            using (var unitOfWork = new TransactionService())
            {
                try
                {
                    unitOfWork.BeginTransaction();

                 
                    string savedFileName = await fileUtility.SaveFileToDirectory(request.Image, Constants.MEAL_IMG_DIR);

                    Dish dishToSave = request.toDish();
                    
                    dishToSave.Image = savedFileName;

                    dishRepository.Add(dishToSave);
                    unitOfWork.Commit();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    unitOfWork.Rollback();
                    return View();
                }
            }
        }

        [HttpPost]
        [Route("Meal/Delete/{id}")]
        [Authorize(Roles = $"{Constants.ADMIN},{Constants.COMPANY}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Dish dishFromDb=dishRepository.Get(d=>d.Id == id);

                if (dishFromDb == null) { return BadRequest(); }

                fileUtility.DeleteFile(dishFromDb.Image,Constants.MEAL_IMG_DIR);

                dishFromDb.IsDeleted = true;
                dishFromDb.Image = null;
                dishRepository.Update(dishFromDb);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) { 
                
                Console.WriteLine(ex.ToString());
                return RedirectToAction("Index", "Dish");
            }
            
        }

        [HttpGet]
        [Route("Meal/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var dish = dishRepository.GetDishAndReviewAndUserAndRating(id);
            var userId = await principal.GetCurrentUserIdAsync();

            var userRating = ratingRepository.Get(r => r.UserId == userId && r.DishId == dish.Id);

            if (dish == null)
            {
                return NotFound();
            }

            var averageRating = dish.Ratings.Count > 0 ? dish.Ratings.Average(r => r.RatingValue) : 0;
            ViewBag.AverageRating = averageRating;
            ViewBag.UserRating = userRating?.RatingValue ?? 0;

            return View("~/Views/Home/Details.cshtml", dish);
        }

    }
}
