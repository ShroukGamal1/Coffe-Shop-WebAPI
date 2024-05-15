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
        public CategoryServices CategoryServices;
        public ProductController(ProductServices Services, CategoryServices categoryServices)
        {
            this.Services = Services;
            CategoryServices = categoryServices;
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
        public ActionResult Add(AddProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("no content");
            }
            Services.Add(product);
            Services.Save();
            return Ok(product);
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
            if (ModelState.IsValid)
            {
                if (id != productDTO.Id  || id == 0)
                {
                    return BadRequest("id is null");
                }
                else
                {
                    Services.Update(productDTO);
                    Services.Save();
                    return CreatedAtAction("getById", new { id = productDTO }, productDTO);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpGet]
        [Route("productPage")]
        public ActionResult Get([FromQuery] string searchTerm = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                List<ProductDTO> productsDTO = Services.getProductPage(searchTerm, page, pageSize);
                return Ok(productsDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetProductsperCategory/{id}")]
        public ActionResult GetProducts(int id)
        {
            if (id== null)
            {
                return NotFound();
            }
            return Ok(Services.getCategoryProducts(id));
        }
        [HttpGet("Get3TopRatedProducts")]
        public ActionResult GetTopRated() {
            return Ok(Services.getTopRated());
        
        }
    }
    }

