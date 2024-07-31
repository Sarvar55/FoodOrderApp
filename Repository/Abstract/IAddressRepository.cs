using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.EntityFramework;

namespace FoodOrderApp.Repository.Abstract
{
    public interface IAddressRepository:IEntityRepository<Address>
    {
    }
}
