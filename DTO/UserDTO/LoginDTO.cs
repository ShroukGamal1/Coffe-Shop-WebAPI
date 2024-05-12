using System.ComponentModel.DataAnnotations;

namespace Coffe_Shop_WebAPI.DTO.UserDTO
{
    public class LoginDTO
    {
        public LoginDTO()
        {
            
        }
        public LoginDTO(string Email, string Password, bool RememberMe)
        {
            this.Email=Email;
            this.Password=Password;
            this.RememberMe = RememberMe;
        }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
