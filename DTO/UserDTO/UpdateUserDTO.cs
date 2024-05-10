namespace Coffe_Shop_WebAPI.DTO.UserDTO
{
    public class UpdateUserDTO
    {
        public UpdateUserDTO()
        {

        }
        public UpdateUserDTO(string Id, string Name, string Email, string Phone,string Address)
        {
            this.Id = Id;
            this.Name = Name;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
