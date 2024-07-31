using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.Domains.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace FoodOrderApp.Data
{
    public class ContextSeed
    {
        public async static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<FoodOrderContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                await SeedRolesAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
        

        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<Role> roleManager) {
            var AdminRole = new Role(Roles.ADMIN.ToString());
            var userRole=  new Role(Roles.USER.ToString());
            var CompanyRole=new Role(Roles.COMPANY.ToString());

            if (!await roleManager.RoleExistsAsync(AdminRole.Name))
            {
                await roleManager.CreateAsync(AdminRole);
            }
            if (!await roleManager.RoleExistsAsync(userRole.Name))
            {
                await roleManager.CreateAsync(userRole);
            }
            if (!await roleManager.RoleExistsAsync(CompanyRole.Name))
            {
                await roleManager.CreateAsync(CompanyRole);
            }
        }
    }
}
