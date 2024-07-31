namespace FoodOrderApp.Models.Domains
{
    public class Courier:BaseEntity
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<Order> Orders { get; set; }
    }
}
