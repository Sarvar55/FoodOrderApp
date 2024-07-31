using FoodOrderApp.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Security
{
    public class PrincipalHelper
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PrincipalHelper(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<User> GetCurrentUserAsync()
        {
            return userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.Id;
        }
    }
}
