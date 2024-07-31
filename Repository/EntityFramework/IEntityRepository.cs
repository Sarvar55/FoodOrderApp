using FoodOrderApp.Models.Domains;
using System.Linq.Expressions;

namespace FoodOrderApp.Repository.EntityFramework
{
    public interface IEntityRepository<T> where T : class,IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> filter);
    }
}
