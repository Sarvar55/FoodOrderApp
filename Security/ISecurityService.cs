using FoodOrderApp.Models.DTO;
using FoodOrderApp.Models.ViewModels;

namespace FoodOrderApp.Security
{
    public interface ISecurityService
    {
        Task<Status> LoginAsync(LoginRequest request);


        Task<Status> RegisrationAsync(RegisterRequest request);

        Task LogoutAsync();
    }
}
