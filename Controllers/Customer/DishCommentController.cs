using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Security;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace FoodOrderApp.Controllers.Customer
{
    [Authorize(Roles = $"{Constants.USER},{Constants.COMPANY}")]
    public class DishCommentController : Controller

    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly IDishRepository dishRepository;
        private readonly IDishCommentRepository commentRepository;
        private readonly IRatingRepository ratingRepository;
        private readonly PrincipalHelper principal;

        public DishCommentController(IUserRepository userRepository, UserManager<User> userManager, IDishRepository dishRepository, IDishCommentRepository commentRepository, IRatingRepository ratingRepository, PrincipalHelper principal)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.dishRepository = dishRepository;
            this.commentRepository = commentRepository;
            this.ratingRepository = ratingRepository;
            this.principal = principal;
        }

        [HttpPost]
        [Route("Dish/Comment/Add")]
        public async Task<IActionResult> Comment(int dishId, string comment)
        {
            string userId = await principal.GetCurrentUserIdAsync();

            if (userId == null)
            {
                return Unauthorized();
            }

            var dish = dishRepository.Get(d => d.Id == dishId);
            if (dish == null)
            {
                return NotFound();
            }

            var dishComment = new DishComment
            {
                UserId = userId,
                DishId = dishId,
                DishComments = comment
            };

           commentRepository.Add(dishComment);
          
            return RedirectToAction(nameof(Details), new { id = dishId });
        }

    
    }
}

