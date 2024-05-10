using System.ComponentModel.DataAnnotations.Schema;

namespace Coffe_Shop_WebAPI.Models
{
    public class Product_User
    {
        [ForeignKey("Product")]
        public int ProductId {  get; set; }
        [ForeignKey("User")]
        public string? UserId {  get; set; }
        public Product Product {  get; set; }
        public AppUser User { get; set; }
            
    }
}
