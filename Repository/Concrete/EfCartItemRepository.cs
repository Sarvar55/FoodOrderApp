using FoodOrderApp.Data;
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Repository.Concrete
{
    public class EfCartItemRepository : EfEntityRepositoryBase<CartItem, FoodOrderContext>, ICartItemRepository
    {
        public List<CartItem> GetItemsAndDishsByCartId(int cartId)
        {
            using (var context = new FoodOrderContext()) { 
            
                return context.cartItems.Include(x => x.Dish).Where(c=>c.CartId==cartId&& c.IsDeleted!=true).ToList();
            }
        }
    }
}
