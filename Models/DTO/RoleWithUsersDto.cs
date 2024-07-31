namespace FoodOrderApp.Models.DTO
{
    public class RoleWithUsersDto
    {
        public string RoleId { get; set; }

        public string Name { get; set; }

        public List<Object>? Values { get; set; }=new List<object>(){ };

        public RoleWithUsersDto(string roleId, string name)
        {
            RoleId = roleId;
            Name = name;
        }
    }
}
