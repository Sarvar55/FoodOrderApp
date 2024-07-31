using FoodOrderApp.Config;

namespace FoodOrderApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration();
            services.AddDatabaseConfiguration(Configuration);
            services.AddApplicationServices();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseExceptionHandling(env);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=UserIndex}/{id?}");
            
        }
    }
}
