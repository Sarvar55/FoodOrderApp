using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Security;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers.Customer
{
    [Authorize(Roles = $"{Constants.USER},{Constants.COMPANY}")]
    public class RatingController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly IRatingRepository ratingRepository;
        private readonly IDishRepository dishRepository;
        private readonly PrincipalHelper principal;

        public RatingController(UserManager<User> userManager, IRatingRepository ratingRepository, IDishRepository dishRepository, PrincipalHelper principal)
        {
            this.userManager = userManager;
            this.ratingRepository = ratingRepository;
            this.dishRepository = dishRepository;
            this.principal = principal;
        }

        [HttpPost]
        [Route("Dish/Rating/Add")]
        public async Task<IActionResult> Rating(int dishId, int ratingValue)
        {
            var userId = await principal.GetCurrentUserIdAsync();

            if (userId == null)
            {
                return Unauthorized();
            }

            var dish = dishRepository.Get(d => d.Id == dishId);
            if (dish == null)
            {
                return NotFound();
            }

            var userRating=ratingRepository.Get(r=>r.UserId== userId && r.DishId==dishId);

            if (userRating != null)
            {
                userRating.RatingValue = AdjustRatingValue(dishId, ratingValue);
                ratingRepository.Update(userRating);
                
            }
            else
            {
                var rating = new Rating
                {
                    UserId = userId,
                    DishId = dishId,
                    RatingValue = AdjustRatingValue(dishId, ratingValue),
                    RatingDate = DateTime.Now
                };

                ratingRepository.Add(rating);
            }
            
            return Json(new { success = true });
        }

        private int AdjustRatingValue(int dishId, int ratingValue)
        {
            var existingRatings = ratingRepository.GetAll(r => r.DishId == dishId).ToList();
            if (existingRatings.Count == 0)
            {
                return ratingValue;
            }

            double currentAverage = existingRatings.Average(r => r.RatingValue);
            double newAverage = (currentAverage * existingRatings.Count + ratingValue) / (existingRatings.Count + 1);

            if (newAverage > 5)
            {
                ratingValue = (int)((5 * (existingRatings.Count + 1) - currentAverage * existingRatings.Count) / 2);
            }

            return ratingValue > 5 ? 5 : ratingValue;
        }
    }
}
