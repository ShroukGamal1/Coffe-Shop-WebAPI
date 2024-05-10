namespace Coffe_Shop_WebAPI.DTO.ProductDTO
{
    public class AddProductDTO
    {
        public AddProductDTO()
        {

        }
        public AddProductDTO( string Name, string Description, decimal Price, double Rating, int Quantity, int? categoryId,string image)
        {
           
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Rating = Rating;
            this.Quantity = Quantity;
            Image=image;
            CategoryId = categoryId;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
        public string Image { get; set; }
    }
}
