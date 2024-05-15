using Coffe_Shop_WebAPI.DTO.CartDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.UnitOfWork;

namespace Coffe_Shop_WebAPI.Services
{
    public class CartServices
    {
        unitOfWork<Product_Order> UnitOfWork;
        public CartServices(unitOfWork<Product_Order> UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public List<Product_Order> GetAll()
        {
            List<Product_Order> products = UnitOfWork.Entity.GetAll();
            if (products == null)
            {
                return null;
            }
            else
            {
               
                return products;
            }
        }

        public List<cartDTO> Get(int orderId)
        {
            List<cartDTO> carts = new List<cartDTO>(); 
            List<Product_Order>p=UnitOfWork.Entity.getElements(c=>c.OrderId==orderId,null);
            foreach(var pro in p)
            {
                cartDTO c = new cartDTO(pro.ProductId,pro.OrderId,pro.Quantity,pro.SubPrice);
                carts.Add(c);
            }
            if (carts == null)
            {
                return null;
            }
            else
            {
               
                return carts;
            }
        }
        public void Delete(cartDTO cart)
        {
            Product_Order product = UnitOfWork.Entity.getElement(c => c.ProductId == cart.ProductId && c.OrderId == cart.OrderId,null);

            UnitOfWork.Entity.Delete(product);
        }
        public void Add(cartDTO cartDTO)
        {
            if (cartDTO != null)
            {
                Product_Order product = new Product_Order()
                {
                    ProductId=cartDTO.ProductId,
                    OrderId=cartDTO.OrderId,
                    Quantity=cartDTO.Quantity,
                    SubPrice=cartDTO.SubPrice
                    
                };
                UnitOfWork.Entity.Add(product);
            }
        }

        public void Update(cartDTO productDTO)
        {
            if (productDTO != null)
            {
                
                Product_Order product = new Product_Order()
                {
                  ProductId=productDTO.ProductId,
                  OrderId=productDTO.OrderId,
                  Quantity=productDTO.Quantity,
                  SubPrice=productDTO.SubPrice
                };
                UnitOfWork.Entity.Update(product);
            }
        }

      

        public void Save()
        {
            UnitOfWork.Entity.Save();
        }
    }
}
