using System.ComponentModel.DataAnnotations.Schema;

namespace Coffe_Shop_WebAPI.DTO.OrderDTO
{
    public class OrderDTO
    {
        public OrderDTO() { }
        public OrderDTO(int Id, char State, DateTime CheckOutDate, decimal TotalPrice, string UserId)
        {
            this.Id = Id;
            this.State = State;
            this.CheckOutDate = CheckOutDate;
            this.TotalPrice = TotalPrice;
            this.UserId = UserId;
        }
        public int Id { get; set; }
        public char State { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
    }
}
