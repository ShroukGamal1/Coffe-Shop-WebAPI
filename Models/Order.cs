using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffe_Shop_WebAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public char State {  get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice {  get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual AppUser User { get; set; }
        public List< Product_Order> ProductOrders {  get; set; }

    }
}
