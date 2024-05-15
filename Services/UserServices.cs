using Coffe_Shop_WebAPI.DTO.UserDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace Coffe_Shop_WebAPI.Services
{
    public class UserServices
    {
        public unitOfWork<AppUser> unit;
        public UserServices(unitOfWork<AppUser> unit)
        {
            this.unit = unit;
        }

        public List<UserDTO> GetAll()
        {
            List<AppUser> users = unit.Entity.GetAll();
            List<UserDTO> usersDTO = new List<UserDTO>();
            if (users == null)
            {
                return null;
            }
            else
            {
                foreach (var Pro in users)
                {
                    UserDTO userDTO = new UserDTO(Pro.Id,Pro.UserName,Pro.Email,Pro.PasswordHash,Pro.PhoneNumber);

                    usersDTO.Add(userDTO);
                }
                return usersDTO;
            }
        }
    
        public UserDTO GetById(string id)
        {

            AppUser p =  unit.Entity.getElement(p => p.Id == id, null);
            return new UserDTO(p.Id, p.UserName, p.Email, p.PasswordHash, p.PhoneNumber);
        }

        public void Update(UserDTO User)
        {
            AppUser p = new AppUser()
            {
                Id = User.Id,
                UserName = User.Name,
                Email = User.Email,
                PasswordHash = User.Password,
                PhoneNumber = User.Phone,

            };
            unit.Entity.Update(p);
        }
        public void Delete(string id)
        {
            AppUser User = unit.Entity.getElement(p => p.Id == id, null);
            unit.Entity.Delete(User);
        }
        public void Add(UserDTO User)
        {
            AppUser p = new AppUser()
            {
                Id = User.Id,
                UserName = User.Name,
                Email = User.Email,
                PasswordHash = User.Password,
                PhoneNumber = User.Phone,
            };
            unit.Entity.Add(p);
        }
        public void Save()
        {
            unit.Entity.Save();
        }
        
    }
}
