namespace Coffe_Shop_WebAPI.DTO.FavouriteDTO
{
    public class UserFavFoodDTO
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
    }
}
