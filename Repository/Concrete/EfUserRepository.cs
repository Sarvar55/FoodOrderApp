using FoodOrderApp.Data;
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Repository.Concrete
{
    public class EfUserRepository : EfEntityRepositoryBase<User, FoodOrderContext>, IUserRepository
    {
        public User GetUserAndRoles(string userId)
        {
            using var context = new FoodOrderContext();
            return context.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == userId);
           
        }
    }
}
