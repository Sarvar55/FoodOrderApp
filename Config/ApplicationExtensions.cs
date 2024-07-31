using FoodOrderApp.Data;

namespace FoodOrderApp.Config
{
    public static class ApplicationExtensions
    {
        public static  IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                ContextSeed.Seed(app);
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            return app;
        }
    }
}
