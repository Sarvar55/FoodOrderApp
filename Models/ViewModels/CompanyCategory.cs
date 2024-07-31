using FoodOrderApp.Models.Domains;

namespace FoodOrderApp.Models.ViewModels
{
    public class CompanyCategory
    {
        public List<Category> Categories { get; set; }  

        public List<Company> Companys { get; set;}
    }
}
