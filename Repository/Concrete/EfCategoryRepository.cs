using FoodOrderApp.Data;
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Repository.Concrete
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category, FoodOrderContext>, ICategoryRepository
    {
        public List<Category> GetCategoriesAndDishes()
        {
            using (var context = new FoodOrderContext()) {

            return context.Categories.Include(c=>c.Dishes.Where(d=>d.IsDeleted!=true)).ToList();

            }
        }

        public List<Category> GetCategoriesByCompanyId(int companyId)
        {
            using (var context = new FoodOrderContext())
            {
                return context.Categories.Where(c=>c.CompanyId==companyId && c.IsDeleted!=true).Include(c=>c.Dishes).ToList();
            }
        }

        public Category GetCategoryAndDishes(int categoryId)
        {
           using(var context = new FoodOrderContext())
            {
                return context.Categories.Include(c => c.Dishes.Where(d => d.IsDeleted != true)).FirstOrDefault(c=>c.Id==categoryId);
            }
        }
        
    }
}
