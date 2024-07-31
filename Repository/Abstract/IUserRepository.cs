using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.EntityFramework;

namespace FoodOrderApp.Repository.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        User GetUserAndRoles(string userId);

    }
}
