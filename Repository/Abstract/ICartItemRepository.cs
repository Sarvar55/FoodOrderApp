using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.EntityFramework;

namespace FoodOrderApp.Repository.Abstract
{
    public interface ICartItemRepository:IEntityRepository<CartItem>
    {
        List<CartItem> GetItemsAndDishsByCartId(int cartId);
    }
}
