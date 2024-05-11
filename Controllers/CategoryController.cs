using Coffe_Shop_WebAPI.DTO.CategoryDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_Shop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       public CategoryServices services;
        public CategoryController(CategoryServices services)
        {
            this.services = services;
        }
        [HttpGet]
            public ActionResult Get()
            {
                List<CategoryDTO> categories = services.GetAll();
                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            [HttpGet("{id}")]
            public ActionResult getById(int id)
            {
                CategoryDTO categoryDTO = services.Get(id);
                if (categoryDTO == null)
                {
                    return NotFound();
                }
                return Ok(categoryDTO);
            }
            [HttpPost]
            [Route("category")]
            public ActionResult Add(AddCategoryDTO categoryDTO)
            {
                if (categoryDTO == null)
                {
                    return BadRequest();
                }
                services.Add(categoryDTO);
                services.Save();
                return Ok(categoryDTO);
            }

            [HttpDelete("{id}")]
            public ActionResult deleteCategory(int id)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                else
                {
                    services.Delete(id);
                    services.Save();
                    return Ok();
                }
            }

            [HttpPut("{id}")]
            public ActionResult update(int id,CategoryDTO categoryDTO)
            {

            if (id != categoryDTO.Id|| categoryDTO == null||id==0) { return BadRequest(); }
            
                else
                {
                    services.Update(categoryDTO);
                    services.Save();
                    return CreatedAtAction("getById", new { id = categoryDTO }, categoryDTO);
                }

            }
        }
    }