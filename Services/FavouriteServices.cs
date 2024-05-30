using Coffe_Shop_WebAPI.DTO.FavouriteDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.UnitOfWork;

namespace Coffe_Shop_WebAPI.Services
{
    public class FavouriteServices
    {
        unitOfWork<Product_User> UnitOfWork;
        public FavouriteServices(unitOfWork<Product_User> UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public List<FavouriteFoodDTO> GetAll()
        {
            List<Product_User> products = UnitOfWork.Entity.GetAll();
            List<FavouriteFoodDTO> productsDTO = new List<FavouriteFoodDTO>();
            if (products == null)
            {
                return null;
            }
            else
            {
                foreach (var Pro in products)
                {
                    FavouriteFoodDTO ProductDTO = new FavouriteFoodDTO(Pro.ProductId,Pro.UserId);

                    productsDTO.Add(ProductDTO);
                }
                return productsDTO;
            }
        }

        public FavouriteFoodDTO Get(int productId, string userId, string include)
        {
            Product_User FavouriteFood = UnitOfWork.Entity.getElement(c => c.ProductId == productId && c.UserId == userId, include);
            if (FavouriteFood== null)
            {
                return null;
            }
            else
            {
                FavouriteFoodDTO FavouriteDTO = new FavouriteFoodDTO(FavouriteFood.ProductId,FavouriteFood.UserId);
                return FavouriteDTO ;
            }
        }
        public void Delete(FavouriteFoodDTO favourite)
        {
            Product_User product = UnitOfWork.Entity.getElement(c => c.ProductId == favourite.ProductId && c.UserId == favourite.UserId,null);

            UnitOfWork.Entity.Delete(product);
        }
        public void Add(FavouriteFoodDTO FavouriteDTO)
        {
            if (FavouriteDTO != null)
            {
                Product_User product = new Product_User()
                {
                   ProductId=FavouriteDTO.ProductId,
                   UserId=FavouriteDTO.UserId,
                };
                UnitOfWork.Entity.Add(product);
            }
        }

        public void Update(FavouriteFoodDTO productDTO)
        {
            if (productDTO != null)
            {
                Product_User product = new Product_User()
                {
                    ProductId = productDTO.ProductId,
                    UserId=productDTO.UserId
                };
                UnitOfWork.Entity.Update(product);
                
            }
        }
        public void Save()
        {
            UnitOfWork.Entity.Save();
        }
        public FavouriteFoodDTO GetById(int prodId, string userId, string include)
        {

            Product_User p = UnitOfWork.Entity.getElement(p => p.ProductId == prodId && p.UserId == userId, include);

            return new FavouriteFoodDTO(p.ProductId, p.UserId);
        }
        public List<UserFavFoodDTO> GetUserFav(string userId, string include)
        {
            var res = UnitOfWork.Entity.getElements(x => x.UserId == userId, include);

            List<UserFavFoodDTO> userFav = new List<UserFavFoodDTO>();
            foreach (var item in res)
            {
                UserFavFoodDTO userFavFoodDTO = new UserFavFoodDTO()
                {
                    UserId = item.UserId,
                    ProductId = item.ProductId,
                    ProductDesc = item.Product.Description,
                    ProductImage = item.Product.image,
                    ProductName = item.Product.Name,
                    ProductPrice = item.Product.Price,
                };
                userFav.Add(userFavFoodDTO);
            }

            return userFav;
        }
    }
}
