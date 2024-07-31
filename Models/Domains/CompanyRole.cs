namespace FoodOrderApp.Models.Domains
{
    public class CompanyRole
    {
        public int CompanyId {  get; set; }

        public int RoleId {  get; set; }

        public Role Role { get; set; }
        
        public Company Company { get; set; }
    }
}
