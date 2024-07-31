using FoodOrderApp.Models.Domains;

namespace FoodOrderApp.Models.DTO
{
    public class UserDto
    {
        public string Id {  get; set; }
        
        public string UserName { get; set; }  
        public string Email { get; set; }

        public string Password { get; set; }
        public string LastName { get; set; }

        public string RoleName {  get; set; }

        public List<Address>? Address { get; set; }

    }
}
