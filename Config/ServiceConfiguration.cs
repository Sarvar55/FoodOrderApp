using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.Concrete;
using FoodOrderApp.Security;
using FoodOrderApp.Services.Transaction;
using FoodOrderApp.Services.User;
using FoodOrderApp.Utility;
using System.Transactions;

namespace FoodOrderApp.Config
{
    public static class ServiceConfiguration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddSingleton<IUserRepository, EfUserRepository>();
            services.AddSingleton<ICategoryRepository, EfCategoryRepository>();
            services.AddSingleton<ICompanyRepository, EfCompanyRepository>();
            services.AddSingleton<IDishRepository,EfDishRepository>();
            services.AddSingleton<IAddressRepository, EfAddressRepository>();
            services.AddSingleton<ICompanyAddressRepository, EfCompanyAddressRepository>();
            services.AddSingleton<IRoleRepository, EfRoleRepository>();
            services.AddSingleton<ICartItemRepository, EfCartItemRepository>();
            services.AddSingleton<ICartRepository, EfCartRepository>(); 
            services.AddSingleton<IDishCommentRepository, EfDishCommentRepository>();
            services.AddSingleton<IRatingRepository,EfRatingRepository>();

            services.AddHttpContextAccessor();

            services.AddScoped<PrincipalHelper>();
            services.AddScoped<FileUtility>();
            services.AddScoped<TransactionService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<ISecurityService, SecurityService>();

            return services;
        }
    }
}
