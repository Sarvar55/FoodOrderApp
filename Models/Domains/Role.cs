using Microsoft.AspNetCore.Identity;

namespace FoodOrderApp.Models.Domains
{
    public class Role:IdentityRole,IEntity
    {
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Role(string Name):this()
        {
            this.Name = Name;
        }
        public Role()
        {
            this.Users = new List<User>();
            
        }
    }
}
