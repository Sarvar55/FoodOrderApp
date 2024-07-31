using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.EntityFramework;

namespace FoodOrderApp.Repository.Abstract
{
    public interface IDishRepository : IEntityRepository<Dish>
    {
        List<Dish> GetDishesAndCategoryByCompanyId(int companyId);

        List<Dish> GetDishesByCategoryId(int categoryId);

        List<Dish> GetDishesWithCategoryAndCompany();

        Dish GetDishAndReviewAndUserAndRating(int dishId);
    }
}
