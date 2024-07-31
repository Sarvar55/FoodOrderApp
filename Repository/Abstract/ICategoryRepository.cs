using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.EntityFramework;

namespace FoodOrderApp.Repository.Abstract
{
    public interface ICategoryRepository:IEntityRepository<Category>
    {
        List<Category> GetCategoriesByCompanyId(int companyId);

        Category GetCategoryAndDishes(int categoryId);

        List<Category> GetCategoriesAndDishes();
    }
}
