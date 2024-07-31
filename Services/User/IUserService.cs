using FoodOrderApp.Models.DTO;
using FoodOrderApp.Models.ViewModels;

namespace FoodOrderApp.Services.User
{
    public interface IUserService
    {
        Task<Status> CreateUserAsync(CreateUserRequest request);


    }
}
