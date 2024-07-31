namespace FoodOrderApp.Models.Domains
{
    public class OrderDetail:BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
