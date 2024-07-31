using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.Models.DTO;
using System.Net;
using FoodOrderApp.Security;
using FoodOrderApp.Services.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FoodOrderApp.Utility.Constants;
using FoodOrderApp.Repository.Abstract;
namespace FoodOrderApp.Controllers.Admin
{
    [Authorize(Roles =Constants.ADMIN)]
    [Route("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly RoleManager<Role> roleManager;
        private readonly IUserRepository userRepository;
        public UserController(UserManager<User> userManager, IUserRepository userRepository, IUserService userService, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.roleManager = roleManager;
            this.userRepository=userRepository;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> Index()
        {
            var usersList = await userManager.Users.Where(u=>u.IsDeleted!=true)
                .Include(u => u.Addresses)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    LastName = u.Lastname,
                    Address = u.Addresses,
                })
                .ToListAsync();

            foreach (var user in usersList)
            {
                var roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.Id));
                user.RoleName = roles.FirstOrDefault() ?? "No Role";
            }

            return View("~/Views/Admin/User/Index.cshtml", usersList);
        }


        [HttpGet]
        [Route("Users/Create")]
        public IActionResult Create()
        {
           
            ViewData["Roles"] = roleManager.Roles.ToList();
            return View("~/Views/Admin/User/Create.cshtml");
        }

        [HttpPost]
        [Route("Users/Create")]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            var result = await userService.CreateUserAsync(request);
            TempData["msg"] = result.Message;
           
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Users/Delete/{id}")]

        public async Task<string> Delete(string id)
        {
            User user = userManager.Users.SingleOrDefault(u => u.Id == id);
           
            if (user != null)
            {
              user.IsDeleted = true;
                
              await userManager.UpdateAsync(user);
              
                return user.UserName + " " + user.Lastname;
            }
            return "";
        }

        [HttpPost]
        [Route("Users/Update")]
        public async Task<IActionResult> Update(UserDto user)
        {
            if(user==null && user.Id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            User userToUpdate = await userManager.FindByIdAsync(user.Id);

            if (userToUpdate != null)
            {
                userToUpdate.UserName = user.UserName;
                userToUpdate.Lastname = user.LastName;
                userToUpdate.Email = user.Email;
               await userManager.UpdateAsync(userToUpdate);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Users/Detail/{id}")]

        public async Task<IActionResult> Details(string id)
        {
            User user = userRepository.GetUserAndRoles(id);
            UserDto userDto=user.toDto();

            if (user.Roles!=null && user.Roles.Count > 0)
            {
                userDto.RoleName = user.Roles[0].Name;
            }

            ViewData["Roles"] = roleManager.Roles.ToList();

            return View("~/Views/Admin/User/UserDetail.cshtml", userDto);
        }

    }
}
