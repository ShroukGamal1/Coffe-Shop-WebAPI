namespace Coffe_Shop_WebAPI.DTO.UserDTO
{
    public class UpdatePasswordDTO
    {
        public UpdatePasswordDTO()
        {

        }
        public UpdatePasswordDTO( string CurrentPassword,string NewPassword)
        {
           
            this.CurrentPassword = CurrentPassword;
            this.NewPassword= NewPassword;
            
        }

       
        public string CurrentPassword { get; set; }
        public string NewPassword {  get; set; }

    }
}
