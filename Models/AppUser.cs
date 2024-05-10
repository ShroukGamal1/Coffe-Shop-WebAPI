using Microsoft.AspNetCore.Identity;

namespace Coffe_Shop_WebAPI.Models
{
    public class AppUser:IdentityUser
    {
       public string Address {  get; set; }
        
        public List<Order> Orders { get; set; }
        public List<Product_User> Favourite { get; set; }
    }
}
