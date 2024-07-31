namespace FoodOrderApp.Models.Domains;

public class Dish : BaseEntity, IEntity
{
    
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public string Image { get; set; }
    public virtual List<OrderDetail> OrderDetails { get; set; }
    public virtual List<DishComment> Reviews { get; set; }
    public virtual List<Rating> Ratings { get; set; }
    public virtual List<CartItem> CartItems { get; set; }

    public Dish()
    {
        OrderDetails = new List<OrderDetail>();
        Reviews = new List<DishComment>();
        Ratings = new List<Rating>();
        CartItems = new List<CartItem>();
    }

}