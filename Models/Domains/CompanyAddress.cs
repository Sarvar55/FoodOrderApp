using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FoodOrderApp.Models.Domains
{
    public class CompanyAddress:BaseEntity
    {
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; }

        public string? Description { get; set; }
        public virtual Company Company { get; set; }
    }
}
