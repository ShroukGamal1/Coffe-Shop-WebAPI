using System.ComponentModel.DataAnnotations.Schema;

namespace Coffe_Shop_WebAPI.DTO.CartDTO
{
    public class cartDTO
    {
        public cartDTO()
        {

        }
        public cartDTO(int ProductId, int OrderId, int Quantity, decimal SubPrice)
        {
            this.ProductId = ProductId;
            this.SubPrice = SubPrice;
            this.OrderId = OrderId;
            this.Quantity = Quantity;
        }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal SubPrice { get; set; }
    }
}
