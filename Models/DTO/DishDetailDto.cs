using FoodOrderApp.Models.Domains;

namespace FoodOrderApp.Models.DTO
{
    public class DishDetailDto
    {
        public Dish Dish { get; set; }
        public int UserRating { get; set; }
    }
}
