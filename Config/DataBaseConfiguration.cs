using FoodOrderApp.Data;
using FoodOrderApp.Utility;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Config
{
    public static class DataBaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoodOrderContext>(optionsAction =>
                optionsAction.UseSqlServer(AppUtil.GetSqlConnectionString(configuration)));
            return services;
        }
    }
}
