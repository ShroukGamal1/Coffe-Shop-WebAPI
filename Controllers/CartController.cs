using Coffe_Shop_WebAPI.DTO.CartDTO;
using Coffe_Shop_WebAPI.DTO.OrderDTO;
using Coffe_Shop_WebAPI.DTO.ProductDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_Shop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        public CartServices Services;
        public ProductServices productServices;

        public CartController(CartServices Services, ProductServices productServices)
        {
            this.Services = Services;
            this.productServices = productServices;
        }
        [HttpGet]
        public ActionResult Get()
        {
            List<Product_Order> orders = Services.GetAll();
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }
        [HttpGet("{Id}")]
        public ActionResult getById(int Id)
        {
            List<cartDTO> orderDTO = Services.Get(Id);
            if (orderDTO == null)
            {
                return NotFound("noo");
            }
            return Ok(orderDTO);
        }

        [HttpPost]
        [Route("cart")]
        public ActionResult Add(cartDTO orderDTO)
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
            public ActionResult delete([FromBody] cartDTO cart)
            {
                if (cart==null)
                {
                    return BadRequest();
                }
                else
                {

                    Services.Delete(cart);
                    Services.Save();
                    return Ok();
                }
            }
        [HttpPut]
        [Route ("update")]
        public ActionResult update(cartDTO productDTO)
        {
            if (productDTO == null || productDTO.OrderId == 0 || productDTO.ProductId == 0)
            {
                return BadRequest();
            }
            else
            {
                var price = productServices.Get(productDTO.ProductId).Price;
               
                    productDTO.SubPrice = productDTO.Quantity * price;
               
                Services.Update(productDTO);
                Services.Save();
                return CreatedAtAction("getById", new { id = productDTO }, productDTO);
            }
        }
    }
    }
