using FoodOrderApp.Models.Domains;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Security;
using FoodOrderApp.Utility.Aspects.Transaction;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    [Authorize(Roles = Constants.USER)]
    [Route("User")]
    public class AddressController : Controller
    {
        private readonly IAddressRepository addressRepository;
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly PrincipalHelper principal;

        public AddressController(IAddressRepository addressRepository, IUserRepository userRepository, UserManager<User> userManager, PrincipalHelper principal)
        {
            this.addressRepository = addressRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.principal = principal;
        }

        [HttpGet]
        [Route("Addresses")]
        public async Task<IActionResult> Index()
        {
            string userId = await principal.GetCurrentUserIdAsync();
            List<Address> addresses= addressRepository.GetAll(a=>a.UserId==userId && a.IsDeleted!=true);

            ViewBag.Addresses = addresses;

            return View("~/Views/Customer/Address/Index.cshtml");
        }

        [HttpGet]
        [Route("Address/Create")]
        public async Task<IActionResult> Create()
        {
         
            return View("~/Views/Customer/Address/Create.cshtml");
        }

        [HttpPost]
        [Route("Address/Create")]
        [TransactionScopeAspect]
        public async Task<IActionResult> Create(Address address)
        {
            if (address == null)
            {
                return BadRequest();
            }
            try
            {
                string userId = await principal.GetCurrentUserIdAsync();
                User user =  userManager.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.Addresses = new List<Address>();
                    user.Addresses.Add(address);
                    userRepository.Update(user);
                    address.UserId = userId;
                    addressRepository.Add(address);
                }
                
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Address");
        }

        [HttpPost]
        [Route("Address/Delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
           
            Address address = addressRepository.Get(a=>a.Id == id); 
            if (address != null)
            {
                try
                {
                    address.IsDeleted = true;
                    addressRepository.Update(address);
                    return Json(address.PostalCode);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }
            }
            return Json(null);

        }
    }
}
