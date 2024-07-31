namespace FoodOrderApp.Models.Domains
{
    public abstract class BaseEntity:IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
