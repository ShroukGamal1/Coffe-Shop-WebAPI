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
            List<Product_Order> orderDTO = Services.Get(Id);
            if (orderDTO == null)
            {
                return NotFound("noo");
            }
            return Ok(orderDTO);
        }

        [HttpPost]
        [Route("cart")]
        public ActionResult Add(CartDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest();
            }
            orderDTO.Quantity = 1;
            var price= productServices.Get(orderDTO.ProductId).Price;
            orderDTO.SubPrice = price * orderDTO.Quantity;
            Services.Add(orderDTO);
            Services.Save();
            return Ok(orderDTO);
        }

        [HttpDelete]
            public ActionResult delete(CartDTO cart)
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
        public ActionResult update(CartDTO productDTO)
        {
            if (productDTO == null || productDTO.OrderId == 0 || productDTO.ProductId == 0)
            {
                return BadRequest();
            }
            else
            {
                var price = productServices.Get(productDTO.ProductId).Price;
                productDTO.SubPrice += (productDTO.Quantity - 1) * price;
                Services.Update(productDTO);
                Services.Save();
                return CreatedAtAction("getById", new { id = productDTO }, productDTO);
            }
        }

        [HttpPut]
        [Route("updateState")]
        public ActionResult updateState(CartDTO productDTO)
        {
            if (productDTO == null || productDTO.OrderId == 0 || productDTO.ProductId == 0)
            {
                return BadRequest();
            }
            else
            {
                ProductDTO product = productServices.Get(productDTO.ProductId);
                if (product.Quantity == productDTO.Quantity)
                {
                    product.Quantity = 0;
                    productServices.Update(product);
                }
                else if (product.Quantity < productDTO.Quantity)
                {
                    return BadRequest();
                }
                else
                {
                    product.Quantity -= productDTO.Quantity;
                    productServices.Update(product);
                }
                Services.UpdateState(productDTO);
            }

            Services.Save();
            return CreatedAtAction("getById", new { id = productDTO }, productDTO);
        }
    }
    }
