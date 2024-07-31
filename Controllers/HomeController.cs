
using FoodOrderApp.Models;
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.Domains.Enums;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Controllers
{

    public class HomeController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IDishRepository dishRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ICompanyRepository companyRepository;
        

        public HomeController(UserManager<User> userManager, RoleManager<Role> roleManager,
            IDishRepository dishRepository, ICategoryRepository categoryRepository, ICompanyRepository companyRepository) 
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dishRepository = dishRepository;
            this.categoryRepository = categoryRepository;
            this.companyRepository = companyRepository;
        }

        [AllowAnonymous]
        [Route("/")]
        [Route("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Dish> dishes = dishRepository.GetAll(d => d.IsDeleted != true);
            ViewBag.Dishes = dishes;
            return View("~/Views/Index.cshtml");
        }

        [HttpGet]
        [Authorize(Roles =Constants.ADMIN)]
        [Route("Admin")]
        [Route("Admin/Index")]
        public IActionResult AdminIndex()
        {
            
            ViewData["rolCount"] = roleManager.Roles.ToList().Count;
            ViewData["userCount"] =userManager.Users.Where(u=>u.IsDeleted!=true).ToList().Count;
            ViewData["restaurantCount"] = companyRepository.GetAll(c=>c.IsDeleted!=true).Count;
            ViewData["categoryCount"] = categoryRepository.GetAll(c => c.IsDeleted != true).Count; 
           
            return View("~/Views/Admin/Index.cshtml");
        }

        [Authorize(Roles =Constants.COMPANY)]
        [Route("Restaurant/Index")]
        [HttpGet]
        public IActionResult RestaurantIndex()
        {

            ViewData["categoryCount"] = categoryRepository.GetAll(c => c.IsDeleted != true).Count;
            ViewData["productCount"] = dishRepository.GetAll(c => c.IsDeleted != true).Count;
           
            ViewData["orderCount"] = 12;// _context.Orders.Where(o => o.CompanyId == currentUserId).Count<Order>();
            ViewData["deliveryCount"] = 12;//_context.Deliveries.Where(d => d.CompanyId == currentUserId).Count<Delivery>();
            ViewData["courierCount"] = 12; //_context.Couriers.Where(c => c.CompanyId == currentUserId).Count<Courier>();

            return View("~/Views/Company/Index.cshtml");
        }

        [Authorize(Roles = Constants.USER)]

        [Route("User/Index")]
        [HttpGet]
        public IActionResult UserIndex()
        {
            List<Company> companies = companyRepository.GetAllCompanyAndAddress();
            ViewBag.Companies = companies;
            return View("~/Views/Customer/Index.cshtml");
        }


        [Route("User/SearchCompanies")]
        [HttpGet]
        public IActionResult SearchCompanies(string search)
        {
            List<Company> companies;

            if (string.IsNullOrEmpty(search))
            {
                companies = companyRepository.GetAllCompanyAndAddress();
            }
            else
            {
                companies = companyRepository.GetAllCompanyAndAddress()
                                              .Where(c => c.CompanyName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                                          c.Address.City.Contains(search, StringComparison.OrdinalIgnoreCase))
                                              .ToList();
            }

            return PartialView("~/Views/Customer/_CompanyList.cshtml", companies);
        }

        [HttpGet]
        [Route("Customer/View")]
        public IActionResult View()
        {
            return View("~/Views/Customer/View.cshtml");
        }

        [HttpGet]
        
        public IActionResult MealMenu()
        {
            List<Dish> dishes = dishRepository.GetAll(d => d.IsDeleted != true);
            ViewBag.Dishes = dishes;
            return View("~/Views/Home/Index.cshtml");
        }

       

        public async Task<IActionResult> Profile()
        {
            User user = await GetCurrentUserAsync();
            if (user == null)
            {
                return NotFound();
            }

            var userWithAddress = await userManager.Users
                .Include(u => u.Addresses.Where(a=>a.IsDeleted!=true))
                .Include(u=>u.Roles)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

          
            IList<string> roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                userWithAddress.Roles.Add(new Role(role));
            }

            return View(userWithAddress);
        }

        private Task<User> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
    }
}
