using System.ComponentModel.DataAnnotations;

namespace Coffe_Shop_WebAPI.DTO.UserDTO
{
    public class RegisterDTO
    {
        public RegisterDTO()
        {

        }
        public RegisterDTO( string Name, string Email, string Password, string Phone, string confirmedPassword, string address)
        {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.Phone = Phone;
            ConfirmedPassword = confirmedPassword;
            Address = address;
        }

        [MinLength (8,ErrorMessage ="You must enter at least 8 characters")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Two password must be the same , Enter the same password.")]
        public string ConfirmedPassword { get; set; }
        public string Address {  get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
