using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.Domains.Enums;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.Concrete;
using FoodOrderApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FoodOrderApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISecurityService securityService;

     public  AuthController(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result= await securityService.LoginAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }

        }
       // [Authorize]
        public async Task<IActionResult> Logout() { 
            await securityService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            request.Role = Roles.USER.ToString();
            var result = await securityService.RegisrationAsync(request);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> RegisterAdmin()
        {
            var model = new RegisterRequest()
            {
                Username = "Elbrus",
                FirstName = "Elbrus",
                LastName = "Musazade",
                Email = "elbrus@gmail.com",
                Password = "Admin1234#",
            };

            model.Role = Roles.ADMIN.ToString();
            var result = await securityService.RegisrationAsync(model);
            return Ok(result);
        }
        public async Task<IActionResult> RegisterCompany()
        {
            var model = new RegisterRequest()
            {
                Username = "Ali55",
                FirstName = "Ali",
                LastName = "Kalantari",
                Email = "ali@gmail.com",
                Password = "Admin1234#",
            };
            model.Role=Roles.COMPANY.ToString();
            var result=await securityService.RegisrationAsync(model);
            return Ok(result);
        }
    }
}
