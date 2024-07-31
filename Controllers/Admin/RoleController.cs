using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.DTO;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Controllers.Admin
{
    [Authorize(Roles =Constants.ADMIN)]
    [Route("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ICompanyRepository companyRepository;

        public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager, ICompanyRepository companyRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.companyRepository = companyRepository;
        }


        [HttpGet]
        [Route("Roles")]
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            List<RoleWithUsersDto> rolesWithUsers = new List<RoleWithUsersDto>();

            foreach (Role role in roles)
            {
                var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);

                RoleWithUsersDto roleUser = new RoleWithUsersDto(role.Id, role.Name);

                if (usersInRole.Any())
                {
                    roleUser.Values = new List<object> { usersInRole };
                }

                rolesWithUsers.Add(roleUser);
            }

            ViewData["restaurantCount"] = companyRepository.GetAll().Count;
            return View("~/Views/Admin/Role/Index.cshtml", rolesWithUsers);
        }

        [HttpPost]
        [Route("Roles/Create")]
        public async Task<IActionResult> Create(CreateRoleRequest request)
        {
            try
            {
             if(! await checkRoleExists(request.Name))
             {
                    await roleManager.CreateAsync(new Role(request.Name));
                    return RedirectToAction(nameof(Index));
             }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(null);
            }
        }
        [HttpPost]
        [Route("Roles/Delete/{Name}")]
        public async Task<IActionResult> Delete(string Name)
        {
            Role role = await roleManager.FindByNameAsync(Name);
            if (role != null)
            {
                try
                {
                 await  roleManager.DeleteAsync(role);
                  
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> checkRoleExists(String Name)
        {
            return await roleManager.RoleExistsAsync(Name);
        }
    }
}
