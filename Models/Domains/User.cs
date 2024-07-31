using FoodOrderApp.Models.Domains.Enums;
using FoodOrderApp.Models.DTO;
using FoodOrderApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FoodOrderApp.Models.Domains
{
    public class User:IdentityUser,IEntity
    {
        public string Lastname { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<DishComment> Reviews { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual List<Role> Roles { get; set; }
            
        public User()
        {
            Addresses = new List<Address>();
            Orders = new List<Order>();
            Reviews = new List<DishComment>();
            Ratings = new List<Rating>();
        
            Roles = new List<Role>();
        }

        public static User convertToUser(CreateUserRequest request)
        {
            User user = new User();
            user.UserName = request.UserName;
            user.Email=request.Email;
            user.PasswordHash = request.Password;
            user.Lastname=request.LastName;
          
            return user;
        }
        public UserDto toDto() => new UserDto()
        {
            Id = this.Id,
            UserName = this.UserName,
            LastName = this.Lastname,
            Email = this.Email,
        };
    }
}
