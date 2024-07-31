using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models.Domains
{
    public class Cart:BaseEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public List<CartItem> CartItems { get; set; }


        public Cart()
        {
            this.CartItems = new List<CartItem>(); 
        }
    }
}
