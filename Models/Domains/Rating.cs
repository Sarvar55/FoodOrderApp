namespace FoodOrderApp.Models.Domains
{
    public class Rating:BaseEntity
        {
            public string UserId { get; set; }
       
            public int DishId { get; set; }
       
            public int RatingValue { get; set; }
            public virtual Dish Dish { get; set; }
            public virtual User User { get; set; }
            public DateTime RatingDate { get; set; }
        }
    }
