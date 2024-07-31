namespace FoodOrderApp.Models.Domains
{
    public class Address:BaseEntity
    {
        public string UserId { get; set; }
        public string? Neighbourhood { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public virtual User User { get; set; }
    }
}
