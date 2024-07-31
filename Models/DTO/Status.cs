using System.Net;

namespace FoodOrderApp.Models.DTO
{
    public class Status
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }
}
