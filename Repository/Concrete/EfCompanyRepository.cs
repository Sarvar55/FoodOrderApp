using FoodOrderApp.Data;
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Repository.Concrete
{
    public class EfCompanyRepository : EfEntityRepositoryBase<Company, FoodOrderContext>, ICompanyRepository
    {
       
        public  List<Company> GetAllCompanyAndAddress()
        {
            using var context = new FoodOrderContext();
            return  context.Companies.Include(c => c.Address).Where(c=>c.IsDeleted!=true).ToList();
           
        }
    }
}
