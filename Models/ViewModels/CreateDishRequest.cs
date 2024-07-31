using FoodOrderApp.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models.ViewModels
{
    public class CreateDishRequest
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public IFormFile Image {  get; set; }

        public int CompanyId {  get; set; }



        public Dish toDish ()
        {
            return new Dish()
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                CategoryId = this.CategoryId,
                CompanyId = this.CompanyId
            };
        }
    }
}
