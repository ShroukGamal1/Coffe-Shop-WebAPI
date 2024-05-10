namespace Coffe_Shop_WebAPI.DTO.ProductDTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {

        }
        public ProductDTO(int Id, string Name, string Description, decimal Price, double Rating, int Quantity, int? categoryId,string size,string image)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Rating = Rating;
            this.Quantity = Quantity;
            Size=size;
            Image=image;
            CategoryId = categoryId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public string ?Size {  get; set; }
        public string Image {  get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
    }
}
