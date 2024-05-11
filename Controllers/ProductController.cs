using Coffe_Shop_WebAPI.DTO.ProductDTO;
using Coffe_Shop_WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_Shop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductServices Services;
        public ProductController(ProductServices Services)
        {
            this.Services = Services;
        }
        [HttpGet]
        public ActionResult Get()
        {
            List<ProductDTO> products = Services.GetAll();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            ProductDTO productDTO = Services.Get(id);
            if (productDTO == null)
            {
                return NotFound();
            }
            return Ok(productDTO);
        }
        [HttpPost]
        [Route("product")]
        public ActionResult Add(AddProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest();
            }
            Services.Add(productDTO);
            Services.Save();
            return Ok(productDTO);
        }

        [HttpDelete]
        [Route("id")]
        public ActionResult delete(int id)
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

        [HttpPut("{id}")]
        public ActionResult update(int id,ProductDTO productDTO)
        {
            if (id != productDTO.Id || productDTO == null || id == 0)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Services.Update(productDTO);
                Services.Save();
                return CreatedAtAction("getById", new { id = productDTO }, productDTO);
            }

        }
    }
    }

