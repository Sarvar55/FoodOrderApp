namespace FoodOrderApp.Models.Domains
{
    public class Company:BaseEntity,IEntity
    {
        public string CompanyName { get; set; }
        public string Logo { get; set; }

        public string About {  get; set; }

        public string Email { get; set; }

        public string Password { get; set; } = null!;

        public string Phone { get; set; }
        
        public virtual CompanyAddress Address { get; set; }
        public virtual ICollection<Category> Categories { get; set; } 

        public virtual List<Courier>? Couriers { get; set; }
        public  virtual List<Order>? Orders { get; set; }
        public virtual List<Dish>? Dishes { get; set; } 

        public Company()
        {
          
            this.Categories = new List<Category>();
            this.Orders = new List<Order>();
            this.Dishes = new List<Dish>();
        }
        
    }
}
