using System.ComponentModel;

namespace FoodOrderApp.Models.ViewModels
{
    public class CreateCategoryRequest
    {
        [DisplayName("Name")]
        public string Name {  get; set; }

        public int CompanyId {  get; set; }
    }
}
