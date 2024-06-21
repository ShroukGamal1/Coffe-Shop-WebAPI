using Coffe_Shop_WebAPI.DTO.UserDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coffe_Shop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserServices userServices;

        public AccountController(UserManager<AppUser>userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = userDTO.Name,
                    PasswordHash = userDTO.Password,
                    PhoneNumber = userDTO.Phone,
                    Email = userDTO.Email,
                    Address = userDTO.Address
                };
                IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    IdentityResult identityResult = await userManager.AddToRoleAsync(user, "User");
                    if (!identityResult.Succeeded)
                    {
                        if (await roleManager.FindByNameAsync("User") == null)
                        {
                            await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                        }
                        else
                            BadRequest();
                    }
                    //create cookies and login
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Created();
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return BadRequest(string.Join(", ", errors));
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        [Route("LogIn")]

        public async Task<IActionResult> Login(LoginDTO userDTO)
        {
            // Check if the provided model is valid
            if (ModelState.IsValid)
            {
                // Find the user by email
                AppUser user = await userManager.FindByEmailAsync(userDTO.Email);

                // if user exist
                if (user != null)
                {
                   
                    bool isValidPassword = await userManager.CheckPasswordAsync(user, userDTO.Password);

                    if (isValidPassword)
                    {
                        string key = "Hello from the other side Shrouq Gamal Ali Shaban";
                        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                        List<Claim> userData = new List<Claim>();
                        userData.Add(new Claim("userId", user.Id));
                        userData.Add(new Claim("userPassword", userDTO.Password));
                        userData.Add(new Claim("username", user.UserName));
                        userData.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                        userData.Add(new Claim(ClaimTypes.Email, user.Email));
                        userData.Add(new Claim("Address", user.Address));
                        var signingcre = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                    claims: userData,
                    signingCredentials: signingcre,
                    expires: DateTime.Now.AddDays(3)
                    );
                        var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);

                        // Sign in the user
                        await signInManager.SignInAsync(user, userDTO.RememberMe);
                        var role = await userManager.GetRolesAsync(user);
                        return Ok(new { role = role, token = tokenstring, user = user });
                    }
                    else
                    {
                        return BadRequest("InCorrect Password.");
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
        [HttpPut("updatePassword")]
        public async Task<IActionResult> UpdatePassword(string userid, UpdatePasswordDTO userDTO)
        {
            var user = await userManager.FindByIdAsync(userid);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                await userManager.ChangePasswordAsync(user, userDTO.CurrentPassword, userDTO.NewPassword);
                await userManager.UpdateAsync(user);
                return Ok(user);
            }
        }
        
        [HttpGet("GettheLoggedInUserClaims")]
        
        public async Task<IActionResult> GetUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                var rols = await userManager.GetRolesAsync(await userManager.FindByIdAsync(id));

                return Ok(new
                {
                    id = id,
                    name = User.Claims.FirstOrDefault(c => c.Type == "username").Value,
                    email = User.Claims.Skip(3).Take(1).FirstOrDefault().Value,
                    password = User.Claims.FirstOrDefault(c => c.Type == "userPassword").Value,
                    address = User.Claims.FirstOrDefault(c => c.Type == "Address").Value,
                    roles = rols
                });
            }
            else
            {
                return Ok(User.Identity.IsAuthenticated);
            }

        }

        //[HttpPut("{id}")]
        //[Route("updateUser")]
        //public async Task<IActionResult> UpdateUser(string id, UpdateUserDTO userDTO)
        //{
        //    var user = await userManager.FindByIdAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        user.Email = userDTO.Email;
        //        user.PhoneNumber = userDTO.Phone;
        //        user.UserName = userDTO.Name;
        //        user.Address = userDTO.Address;
        //        await userManager.UpdateAsync(user);
        //        return Ok(user);
        //    }
        //}




    }
}
