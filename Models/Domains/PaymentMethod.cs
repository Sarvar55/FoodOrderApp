namespace FoodOrderApp.Models.Domains
{
    public class PaymentMethod:BaseEntity
    {
        public string Method { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
