using Coffe_Shop_WebAPI.DTO.CartDTO;
using Coffe_Shop_WebAPI.DTO.FavouriteDTO;
using Coffe_Shop_WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_Shop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        public FavouriteServices Services;
        public FavouriteController(FavouriteServices Services)
        {
            this.Services = Services;
        }
        [HttpGet]
        public ActionResult Get()
        {
            List<FavouriteFoodDTO> orders = Services.GetAll();
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }
        [HttpGet("{id}")]
        public ActionResult getById(string userId, int productId)
        {
            if (productId == null)
            {
                return BadRequest();
            }
            FavouriteFoodDTO? Favourite = Services.GetById(productId, userId,null);
            if (Favourite != null)
            {
                return Ok(Favourite);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("favourite")]
        public ActionResult Add(FavouriteFoodDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest();
            }
            Services.Add(orderDTO);
            Services.Save();
            return Ok(orderDTO);
        }

        [HttpDelete]
        public ActionResult delete(FavouriteFoodDTO fav)
        {
            if (fav == null)
            {
                return BadRequest();
            }
            else
            {
                Services.Delete(fav);
                Services.Save();
                return Ok();
            }
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, FavouriteFoodDTO Favourite)
        {
            if (id == null || Favourite == null || id == 0 || id != Favourite.ProductId)
    {
                return BadRequest();
            }
           else
            {
                Services.Update(Favourite);
                Services.Save();
                return Ok(Favourite);
            }
        }



    }
}
