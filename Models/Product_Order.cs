using System.ComponentModel.DataAnnotations.Schema;

namespace Coffe_Shop_WebAPI.Models
{
    public class Product_Order
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Order")]
        public int OrderId {  get; set; }
        public int Quantity {  get; set; }
        public decimal SubPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

    }
}
