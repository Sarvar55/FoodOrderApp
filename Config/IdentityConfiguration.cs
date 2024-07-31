using FoodOrderApp.Data;
using FoodOrderApp.Models.Domains;
using Microsoft.AspNetCore.Identity;

namespace FoodOrderApp.Config
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<FoodOrderContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuth/Login");
            return services;
        }
    }
}
