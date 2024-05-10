namespace Coffe_Shop_WebAPI.DTO.UserDTO
{
    public class UserDTO
    {
        public UserDTO()
        {

        }
        public UserDTO(string Id, string Name, string Email, string Password, string Phone)
        {
            this.Id = Id;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.Phone = Phone;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

    }
}