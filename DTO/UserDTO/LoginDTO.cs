using System.ComponentModel.DataAnnotations;

namespace Coffe_Shop_WebAPI.DTO.UserDTO
{
    public class LoginDTO
    {
        public LoginDTO()
        {
            
        }
        public LoginDTO(string Username, string Password, bool RememberMe)
        {
            this.Username=Username;
            this.Password=Password;
            this.RememberMe = RememberMe;
        }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
