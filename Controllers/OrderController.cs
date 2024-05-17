using Coffe_Shop_WebAPI.DTO.OrderDTO;
using Coffe_Shop_WebAPI.DTO.ProductDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_Shop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderServices Services;
        public CartServices CartServices;

        public OrderController(OrderServices Services , CartServices CartServices)
        {
            this.Services = Services;
            this.CartServices = CartServices;
        }
        [HttpGet]
        public ActionResult Get()
        {
            List<OrderDTO> orders = Services.GetAll();
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }
        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            OrderDTO orderDTO = Services.Get(id);
            if (orderDTO == null)
            {
                return NotFound();
            }
            return Ok(orderDTO);
        }
        [HttpPost]
        [Route("order")]
        [Authorize]
        public ActionResult Add([FromBody]AddOrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest("Order data is null");
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing");
            }
            orderDTO.UserId = userId;
            var result = Services.Add(orderDTO);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding the order");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
      
        public ActionResult deleteCourse(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                Services.Delete(id);
                Services.Save();
                return Ok();
            }
        }

        [HttpPut]
        [Route("update")]
        public ActionResult update(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id || orderDTO == null || id == 0)
            {
                return BadRequest();
            }
            else
            {
                Services.Update(orderDTO);
                Services.Save();
                return CreatedAtAction("getById", new { id = orderDTO }, orderDTO);
            }
        }

        [HttpGet("GetOrderOfUser")]
        [Authorize]
        public ActionResult GetCart()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            if (userId == null)
            {
                return Unauthorized();
            }
           return Ok(Services.GetOrderWithStateC(userId));
        }


        
    }
}
       
