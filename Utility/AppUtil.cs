using FoodOrderApp.Utility.Constants;

namespace FoodOrderApp.Utility
{
    public class AppUtil
    {
        public static string GetSqlConnectionString(IConfiguration builder) => builder.GetConnectionString(Constants.Constants.CONNECTION_STRING);

        public static IConfiguration GetAppSettingsFile()
        {
           return new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
        }
    }
}
