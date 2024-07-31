using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models.ViewModels
{
    public class CreateCompanyRequest
    {
        [Required]
        public string CompanyName { get; set; }
       
        public IFormFile Logo { get; set; }

        public string About { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        
        public string Password { get; set; } = null!;

        [Required]
        [Phone]
        public string Phone { get; set; }
        public string? Neighbourhood { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string ? Description {  get; set; }
    }
}
