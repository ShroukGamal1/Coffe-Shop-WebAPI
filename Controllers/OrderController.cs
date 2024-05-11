using Coffe_Shop_WebAPI.DTO.OrderDTO;
using Coffe_Shop_WebAPI.DTO.ProductDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_Shop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderServices Services;
        public OrderController(OrderServices Services)
        {
            this.Services = Services;
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
        public ActionResult Add(AddOrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest();
            }
            Services.Add(orderDTO);
            Services.Save();
            return Ok(orderDTO);
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

        
    }
}
       
