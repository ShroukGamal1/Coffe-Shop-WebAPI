using Coffe_Shop_WebAPI.DTO.CategoryDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Coffe_Shop_WebAPI.Services
{
    public class CategoryServices
    {
        unitOfWork<Category> UnitOfWork;
        public CategoryServices(unitOfWork<Category> UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public List<CategoryDTO> GetAll()
        {
            List<Category> categories = UnitOfWork.Entity.GetAll();
            List<CategoryDTO> categoryDTO = new List<CategoryDTO>();
            if (categories == null)
            {
                return null;
            }
            else
            {
                foreach (var Pro in categories)
                {
                    CategoryDTO ProductDTO = new CategoryDTO(Pro.Id,Pro.Name);

                    categoryDTO.Add(ProductDTO);
                }
                return categoryDTO;
            }
        }

        public CategoryDTO Get(int id)
        {
            Category category = UnitOfWork.Entity.GetById(id);
            if (category == null)
            {
                return null;
            }
            else
            {
                CategoryDTO CategoryDTO = new CategoryDTO(category.Id,category.Name);
                return CategoryDTO;
            }
        }
        public void Delete(int id)
        {
           Category category = UnitOfWork.Entity.GetById(id);

            UnitOfWork.Entity.Delete(category);
        }
        public void Add(AddCategoryDTO categoryDTO)
        {
            if (categoryDTO != null)
            {
                Category category= new Category()
                {
                    Name=categoryDTO.Name
                };
                UnitOfWork.Entity.Add(category);
            }
        }

        public void Update(CategoryDTO categoryDTO)
        {
            if (categoryDTO != null)
            {
                Category category = new Category()
                {
                   Id= categoryDTO.Id,
                   Name=categoryDTO.Name
                };
                UnitOfWork.Entity.Update(category);
            }
        }
        public void Save()
        {
            UnitOfWork.Entity.Save();
        }
        public List<string> GetCategoryNamesBy()
        {
            List<string> Categorynames=new List<string>(); 
            foreach(var category in UnitOfWork.Entity.GetAll()) { Categorynames.Add(category.Name); }
            return Categorynames; 
        }

        public string GetCategoryName(int id)
        {

            return UnitOfWork.Entity.GetById(id).Name;
        }

    }
}
