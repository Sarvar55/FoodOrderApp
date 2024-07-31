namespace FoodOrderApp.Models.Domains
{
    public class DishComment:BaseEntity
    {
        public string UserId { get; set; }
       
        public int DishId { get; set; }
        public string DishComments { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual User User { get; set; }
    }
}
