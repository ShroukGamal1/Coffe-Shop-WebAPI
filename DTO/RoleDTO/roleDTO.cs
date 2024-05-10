namespace Coffe_Shop_WebAPI.DTO.RoleDTO
{
    public class roleDTO
    {
        public roleDTO()
        {
            
        }
        public roleDTO(string Id,string Name)
        {
            this.Id= Id;
            this.Name= Name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
