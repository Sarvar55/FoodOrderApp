namespace FoodOrderApp.Models.Domains
{
    public class Order:BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int? CourierId { get; set; }
        public Courier Courier { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string DeliveryAddress { get; set; }
        public int PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
