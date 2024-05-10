namespace Coffe_Shop_WebAPI.DTO.OrderDTO
{
    public class AddOrderDTO
    {
        public AddOrderDTO() { }
        public AddOrderDTO( char State, DateTime CheckOutDate, decimal TotalPrice, string UserId)
        {
            
            this.State = State;
            this.CheckOutDate = CheckOutDate;
            this.TotalPrice = TotalPrice;
            this.UserId = UserId;
        }
        public char State { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
    }
}
