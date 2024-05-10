using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffe_Shop_WebAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating {  get; set; }
        public string ?image {  get; set; }
        public string? size {  get; set; }
        public int Quantity {  get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public List< Product_Order> ProductOrders {  get; set; }
        public List<Product_User> ProductUsers { get; set; }

            

    }
}
