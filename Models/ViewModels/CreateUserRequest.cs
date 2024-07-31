using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models.ViewModels
{
    public class CreateUserRequest
    {
        [Required]

        public string UserName { get; set; }
        [Required]
        public string LastName {  get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? RoleName {  get; set; }
        [Required]
        public string Password { get; set; }

        public string? Neighbourhood { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
    }
}
