namespace FoodOrderApp.Models.Domains;
public class Category : BaseEntity
{
    public string Name { get; set; }
    public int? CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    public virtual List<Dish> Dishes { get; set; }

    public Category()
    {
        Dishes = new List<Dish>();
    }
}