using Coffe_Shop_WebAPI.DTO.ProductDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.UnitOfWork;

namespace Coffe_Shop_WebAPI.Services
{
    public class ProductServices
    {
        unitOfWork<Product> UnitOfWork;
        public ProductServices(unitOfWork<Product>UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public List<ProductDTO> GetAll()
        {
            List<Product>products=UnitOfWork.Entity.GetAll();
            List<ProductDTO>productsDTO = new List<ProductDTO>();
            if (products == null)
            {
                return null;
            }
            else
            {
                foreach (var Pro in products)
                {
                    ProductDTO ProductDTO = new ProductDTO(Pro.Id,Pro.Name,Pro.Description,Pro.Price,Pro.Rating,Pro.Quantity,Pro.CategoryId,Pro.size,Pro.image);
                    
                    productsDTO.Add(ProductDTO);
                }
                return productsDTO;
            }
        }

        public ProductDTO Get(int id)
        {
            Product product = UnitOfWork.Entity.GetById(id);
            if(product == null)
            {
                return null;
            }
            else
            {
                ProductDTO productDTO = new ProductDTO(product.Id, product.Name, product.Description, product.Price, product.Rating, product.Quantity,product.CategoryId,product.size,product.image);
                return productDTO;
            }
        }
        public void Delete(int id)
        {
            Product product=UnitOfWork.Entity.GetById(id);
           
            UnitOfWork.Entity.Delete(product);
        }
        public void Add(AddProductDTO productDTO)
        {
            if (productDTO != null) {
                Product product = new Product()
                {
                    Name=productDTO.Name,
                    Description=productDTO.Description,
                    Price=productDTO.Price,
                    Rating=productDTO.Rating,
                    image=productDTO.Image,
                    Quantity=productDTO.Quantity,
                    CategoryId=productDTO.CategoryId

                };
                UnitOfWork.Entity.Add(product);
                    }
        }

        public void Update(ProductDTO productDTO)
        {
            if (productDTO != null)
            {
                Product product1 = UnitOfWork.Entity.GetById(productDTO.Id);


                product1.Name = productDTO.Name;
                product1.Description = productDTO.Description;
                product1.Price = productDTO.Price;
                product1.Quantity = productDTO.Quantity;
                product1.Rating = productDTO.Rating;
                product1.image = (productDTO.Image != null) ? productDTO.Image : product1.image;
                product1.CategoryId = productDTO.CategoryId;

                
                UnitOfWork.Entity.Update(product1);
            }
        }

        public List<ProductDTO> getProductPage(string searchTerm, int pageNum, int pageSize)
        {
            var products = UnitOfWork.Entity.getElements(p => p.Name == null ? " ".Contains(searchTerm) : p.Name.Contains(searchTerm), null).ToList();
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            foreach (var product in products)
            {
                ProductDTO prodDTO = new ProductDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Rating = product.Rating,
                    Image = product.image,
                    CategoryId = product.CategoryId,

                };

                productsDTO.Add(prodDTO);
            }
            var totalCount = productsDTO.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            productsDTO = productsDTO.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            return productsDTO;
        }

        public void Save()
        {
            UnitOfWork.Entity.Save();
        }
    }
}
