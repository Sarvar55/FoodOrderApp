using FoodOrderApp.Data;
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Repository.Concrete
{
    public class EfDishRepository : EfEntityRepositoryBase<Dish, FoodOrderContext>, IDishRepository
    {
     
        public Dish GetDishAndReviewAndUserAndRating(int dishId)
        {
            using (var context = new FoodOrderContext())
            {
                return context.Dishes.Include(d => d.Reviews).ThenInclude(d => d.User).ThenInclude(u=>u.Ratings).FirstOrDefault(d => d.Id == dishId);
            }
        }

        public List<Dish> GetDishesAndCategoryByCompanyId(int companyId)
        {
            using var context = new FoodOrderContext();
            return context.Dishes.Where(c => c.Id == companyId).Include(c => c.Category).ToList();
        }

        public List<Dish> GetDishesByCategoryId(int categoryId)
        {
            using (var context = new FoodOrderContext()) { 
            
                return context.Dishes.Where(c=>c.CategoryId==categoryId).Include(c=>c.Category).ToList();
            
            }
        }

        public List<Dish> GetDishesWithCategoryAndCompany()
        {
            using (var context = new FoodOrderContext()) { 
            
                return context.Dishes.Include(c=>c.Category).Include(c=>c.Company).ToList();
            }
        }
    }
}
