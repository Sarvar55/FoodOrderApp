using FoodOrderApp.Models.Domains;
using FoodOrderApp.Utility;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Data
{
    public class FoodOrderContext : IdentityDbContext<User>
    {
        public DbSet<Address> UserAddresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<DishComment> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> cartItems { get; set; }  

        public FoodOrderContext()
        {
        }

        public FoodOrderContext(DbContextOptions options) : base(options)
        {
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ConfigureDatabase(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            AddResctictAllEntities(modelBuilder);
        }

        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Added)
                .Select(i=>(BaseEntity)i.Entity);
        }

        private void AddResctictAllEntities(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities) {

            foreach (var entity in entities) { 
                if(entity.CreatedTime==DateTime.MinValue)
                {
                    entity.CreatedTime = DateTime.Now;
                }
            }
        }
        private void RestoreEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = false;
            }
        }

        private void ConfigureDatabase(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = AppUtil.GetAppSettingsFile();
            string connectionString = AppUtil.GetSqlConnectionString(configuration);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
