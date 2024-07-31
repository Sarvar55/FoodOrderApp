using FoodOrderApp.Data;
using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Repository.EntityFramework;

namespace FoodOrderApp.Repository.Concrete
{
    public class EfRatingRepository:EfEntityRepositoryBase<Rating,FoodOrderContext>,IRatingRepository
    {
    }
}
