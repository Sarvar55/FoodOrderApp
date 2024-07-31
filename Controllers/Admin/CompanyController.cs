using Azure.Core;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.Repository.Abstract;
using FoodOrderApp.Services.Transaction;
using FoodOrderApp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using FoodOrderApp.Models.Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderApp.Utility;
using FoodOrderApp.Utility.Aspects.Transaction;

namespace FoodOrderApp.Controllers.Admin
{

    [Authorize(Roles = Constants.ADMIN)]
    [Route("Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository companyRepository;
        private readonly ICompanyAddressRepository companyAddressRepository;
        private readonly UserManager<User> manager;
        private readonly RoleManager<Role> roleManager;
        private readonly FileUtility fileUtility;
        private readonly IRoleRepository roleRepository;

        public CompanyController(ICompanyRepository companyRepository,
            ICompanyAddressRepository companyAddressRepository, IRoleRepository roleRepository, UserManager<FoodOrderApp.Models.Domains.User> manager, FileUtility fileUtility, RoleManager<Role> roleManager)
        {
            this.companyRepository = companyRepository;
            this.manager = manager;
            this.roleManager = roleManager;
            this.companyAddressRepository = companyAddressRepository;
            this.fileUtility = fileUtility;
            this.roleRepository = roleRepository;

        }

        [HttpGet]
        [Route("Restaurants")]
        public IActionResult Index()
        {
            List<FoodOrderApp.Models.Domains.Company> companies = companyRepository.GetAllCompanyAndAddress().Where(c => c.IsDeleted != true).ToList();
            return View("~/Views/Admin/Restaurant/Index.cshtml", companies);
        }

        [HttpGet]
        [Route("Restaurant/Create")]
        public IActionResult Create()
        {
            ViewData["role"] = roleManager.Roles.FirstOrDefault(r => r.Name == "COMPANY");

            return View("~/Views/Admin/Restaurant/Create.cshtml");
        }

        [HttpPost]
        [Route("Restaurants/Create")]
        [TransactionScopeAspect]
        public async Task<IActionResult> Create(CreateCompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Restaurant/Create.cshtml", request);
            }
            try
            {
                string fileName = await fileUtility.SaveFileToDirectory(request.Logo, Constants.COMPANY_IMG_DIR);
                var companyToSave = new Company()
                {
                    Email = request.Email,
                    Logo = fileName,
                    About = request.About,
                    Phone = request.Phone,
                    Password = request.Password,
                    CompanyName = request.CompanyName,
                };
                companyRepository.Add(companyToSave);
               
                var companyAddressToSave = new CompanyAddress()
                {
                    CompanyId = companyToSave.Id,
                    City = request.City,
                    Description = request.Description,
                    PostalCode = request.PostalCode,
                    Country = request.Country,
                };
                companyAddressRepository.Add(companyAddressToSave);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("~/Views/Admin/Restaurant/Create.cshtml", request);
            }
        }


        [HttpPost]
        [Route("Restaurants/Delete/{id}")]

        public async Task<string> Delete(int id)
        {
            Company company = companyRepository.Get(c => c.Id == id);
            if (company != null)
            {

                try
                {

                    company.IsDeleted = true;
                    companyRepository.Update(company);

                    fileUtility.DeleteFile(company.Logo, "foods");

                    var companyAddress = companyAddressRepository.Get(a => a.CompanyId == company.Id);
                    if (companyAddress != null)
                    {
                        companyAddress.IsDeleted = true;
                        companyAddressRepository.Update(companyAddress);
                    }


                }
                catch (IOException ioExp)
                {
                    Console.WriteLine(ioExp.Message);

                }
                return company?.CompanyName;
            }
            return string.Empty;
        }
        
    }

}
