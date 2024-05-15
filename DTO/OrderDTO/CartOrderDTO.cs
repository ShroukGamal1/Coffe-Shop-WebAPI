using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.DTO;
using Coffe_Shop_WebAPI.DTO.CartDTO;
namespace Coffe_Shop_WebAPI.DTO.OrderDTO
{
    public class CartOrderDTO
    {
        public CartOrderDTO() { }
        public CartOrderDTO(int Id, char State, DateTime CheckOutDate, decimal TotalPrice, string UserId,List<cartDTO> Products)
        {
            this.Id = Id;
            this.State = State;
            this.CheckOutDate = CheckOutDate;
            this.TotalPrice = TotalPrice;
            this.UserId = UserId;
            this.Products = Products;
        }
        public int Id { get; set; }
        public char State { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public List<cartDTO> Products {  get; set; }
    }
}

