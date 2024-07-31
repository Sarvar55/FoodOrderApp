using FoodOrderApp.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace FoodOrderApp.Services.Transaction
{
    public class TransactionService : IDisposable
    {
        private readonly FoodOrderContext context;
        private IDbContextTransaction transaction;

        public TransactionService()
        {
            this.context = new FoodOrderContext();
        }

        public void BeginTransaction()
        {
            transaction=context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex) {
                Rollback();
                throw;

            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void Rollback()
        {
            transaction.Rollback();
            transaction.Dispose();
        }

        public void Dispose()
        {
          context.Dispose();
        }
    }
}
