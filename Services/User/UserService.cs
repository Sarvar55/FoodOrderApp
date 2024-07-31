using FoodOrderApp.Models.Domains;
using FoodOrderApp.Models.DTO;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Services.User;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace FoodOrderApp.Models.Domains;

public class UserService :IUserService
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;
    private readonly IAddressRepository addressRepository;

    public UserService(
       
        UserManager<User> userManager,
        RoleManager<Role> roleManager, IAddressRepository addressRepository)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.addressRepository = addressRepository;
    }
    public async Task<Status> CreateUserAsync(CreateUserRequest request)
    {
        var status = new Status();

        if (request == null)
        {
            return new Status
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid request"
            };
        }

        if (await UserExistsAsync(request.Email))
        {
            return new Status
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "User Already Exists"
            };
        }
        var applicationUser = MapToUser(request);
        if (applicationUser == null)
        {
            return new Status
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid user data"
            };
        }

        var result = await userManager.CreateAsync(applicationUser, request.Password);
        if (!result.Succeeded)
        {
            return new Status
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "User creation failed"
            };
        }

        await EnsureRoleExistsAsync(request.RoleName);
        await userManager.AddToRoleAsync(applicationUser, request.RoleName);

        var userAddress = new Address
        {
            UserId=applicationUser.Id,
            Neighbourhood = request.Neighbourhood,
            Street = request.Street,
            City = request.City,
            PostalCode = request.PostalCode
        };
        addressRepository.Add(userAddress);
       
        return new Status
        {
            StatusCode = HttpStatusCode.OK,
            Message = "User has Registered successfully"
        };
    }

    private async Task<bool> UserExistsAsync(string email)
    {
        return await userManager.FindByEmailAsync(email)!=null?true:false;
    }

    private User MapToUser(CreateUserRequest request)
    {
        var user = new User
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = request.Email,
            PasswordHash = request.Password,
            UserName = request.UserName,
            Lastname = request.LastName,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            Addresses = new List<Address>()
        };

        return user;
    }

    private async Task EnsureRoleExistsAsync(string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new Role(roleName));
        }
    }

}
