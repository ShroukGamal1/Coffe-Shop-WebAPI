using System.ComponentModel.DataAnnotations.Schema;

namespace Coffe_Shop_WebAPI.DTO.FavouriteDTO
{
    public class FavouriteFoodDTO
    {
        public FavouriteFoodDTO() { }
        public FavouriteFoodDTO(int ProductId, string? UserId)
        {
            this.ProductId = ProductId;
            this.UserId = UserId;
        }
        public int ProductId { get; set; }
        public string? UserId { get; set; }
    }
}
