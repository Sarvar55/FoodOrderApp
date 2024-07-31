using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models.Domains
{
    public class CartItem:BaseEntity
    {
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public int DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public int Quantity { get; set; }
    }
}
