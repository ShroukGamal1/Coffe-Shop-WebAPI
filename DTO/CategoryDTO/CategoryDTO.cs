namespace Coffe_Shop_WebAPI.DTO.CategoryDTO
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {

        }
        public CategoryDTO(int Id, string Name)
        {
            this.Name = Name;
            this.Id = Id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
