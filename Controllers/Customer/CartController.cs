using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Security;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FoodOrderApp.Controllers.Customer
{
    [Authorize(Roles =Constants.USER)]
    [Route("User")]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly UserManager<User> userManager;
        private readonly ICartItemRepository cartItemRepository;
        private readonly IDishRepository dishRepository;
        private readonly PrincipalHelper principal;

        public CartController(ICartRepository cartRepository, UserManager<User> userManager, ICartItemRepository cartItemRepository, IDishRepository dishRepository, PrincipalHelper principal)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.cartItemRepository = cartItemRepository;
            this.dishRepository = dishRepository;
            this.principal = principal;
        }

        [HttpGet]
        [Route("Cart")]
        public async Task<IActionResult> Index()
        {
            string userId = await principal.GetCurrentUserIdAsync();
            var cart = cartRepository.Get(c => c.UserId == userId);

            if (cart == null)
            {
                
                ViewBag.CartItems = new List<CartItem>();
                ViewBag.CartTotalPrice = 0;
                return View("~/Views/Customer/Cart/Index.cshtml");
            }

            List<CartItem> cartItems = cartItemRepository.GetItemsAndDishsByCartId(cart.Id);
            ViewBag.CartItems = cartItems;
            ViewBag.CartTotalPrice = cartItems.Sum(item => item.Quantity * item.Dish.Price);

            return View("~/Views/Customer/Cart/Index.cshtml");
        }
        [HttpGet]
        [Route("/User/GetCartItemCount")]
        public async Task<IActionResult> GetCartItemCount()
        {
            var userId = await principal.GetCurrentUserIdAsync();
            var cart= cartRepository.Get(c=>c.UserId== userId);
            var cartItemCount = cartItemRepository.GetAll(c => c.CartId == cart.Id).Where(c=>c.IsDeleted!=true).Count();

            return Json(new { count = cartItemCount });
        }

        [HttpPost]
        [Route("Cart/Item/Delete")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var cartItem =  cartItemRepository.Get(c=>c.Id==id);
                if (cartItem == null)
                {
                    return Json(new { success = false });
                }

                cartItem.IsDeleted= true;
                cartItemRepository.Update(cartItem);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("Cart/AddItem")]
        public async Task<IActionResult> AddItem(int dishId)
        {
            if (dishId <= 0)
            {
                return Json(new { success = false });
            }

            try
            {
                string userId = await principal.GetCurrentUserIdAsync();
                var cart = cartRepository.Get(c => c.UserId == userId);
                if (cart == null)
                {
                    cart = new Cart { UserId = userId };
                    cartRepository.Add(cart);
                }

                var dish = dishRepository.Get(d => d.Id == dishId);
                if (dish == null)
                {
                    return Json(new { success = false });
                }

                var existingCartItem = cartItemRepository.Get(d=>d.DishId == dishId && d.IsDeleted!=true);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                    cartItemRepository.Update(existingCartItem);
                }
                else
                {
                    var newCartItem = new CartItem
                    {
                        CartId = cart.Id,
                        DishId = dishId,
                        Quantity = 1
                    };
                    
                    cart.CartItems.Add(newCartItem);
                    cartItemRepository.Add(newCartItem);
                }
                
                cartRepository.Update(cart);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public IActionResult GetCartCount()
        {
            int cartCount = cartItemRepository.GetAll(c => c.IsDeleted != true).Count;

            ViewBag.CartCount = cartCount;

            return View("~/Views/Shared/_UserLayout.cshtml");
        }
    }
}
