namespace Coffe_Shop_WebAPI.DTO.CategoryDTO
{
    public class AddCategoryDTO
    {
        public AddCategoryDTO()
        {

        }
        public AddCategoryDTO(string Name)
        {
            this.Name = Name;

        }
        public string Name { get; set; }
    }
}

