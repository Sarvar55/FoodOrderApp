using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.DTO;
using FoodOrderApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

namespace FoodOrderApp.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly SignInManager<User> signInManager;

        private readonly UserManager<User> userManager;

        private readonly RoleManager<Role> roleManager;

        public SecurityService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Status> LoginAsync(LoginRequest request)
        {
            var status = new Status();

            var user = await userManager.FindByNameAsync(request.Username);

            if (user == null)
            {

                status.StatusCode = HttpStatusCode.BadRequest;
                status.Message = "Invalid Userame";
                return status;

            }
            if (!await userManager.CheckPasswordAsync(user, request.Password))
            {
                status.StatusCode = HttpStatusCode.BadRequest;
                status.Message = "Invalid Password";
                return status;

            }
            var signInResult = await signInManager.PasswordSignInAsync(user, request.Password, false, true);

            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = HttpStatusCode.OK;
                status.Message = "LoggedIn successfully";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = HttpStatusCode.Locked;
                status.Message = "User Locked Out";
                return status;
            }
            else
            {
                status.StatusCode = HttpStatusCode.BadRequest;
                status.Message = "Error on Log";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegisrationAsync(RegisterRequest request)
        {
            var status = new Status();

            var userExists = await userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                status.StatusCode = HttpStatusCode.BadRequest;
                status.Message = "User Already Exists";
                return status;
            }
            User aplicationUser = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = request.Email,
                UserName = request.Username,
                Lastname = request.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await userManager.CreateAsync(aplicationUser, request.Password);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }
            
            if (!await roleManager.RoleExistsAsync(request.Role))
            {
                await roleManager.CreateAsync(new Role(request.Role));
            }
            if (await roleManager.RoleExistsAsync(request.Role))
            {
                await userManager.AddToRoleAsync(aplicationUser, request.Role);
            }
            status.StatusCode = HttpStatusCode.OK;
            status.Message = "User has Registered succesfully";
            return status;
        }
    }
}
