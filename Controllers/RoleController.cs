using Coffe_Shop_WebAPI.DTO.RoleDTO;
using Coffe_Shop_WebAPI.DTO.UserDTO;
using Coffe_Shop_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_Shop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        [HttpPost]
        public async Task<ActionResult> addRole(string roleName)
        {
            if (roleManager == null)
            {
                return BadRequest("role name is null");
            }
            IdentityRole role = new IdentityRole()
            {
                Name = roleName
            };
            await roleManager.CreateAsync(role);
            return Ok(role);
        }
        [HttpPut]
        [Route("Updaterole")]
        public async Task<ActionResult> editRole(roleDTO role)
        {
            if (roleManager == null)
            {
                return BadRequest("role object is null");
            }
            IdentityRole? identityRole = await roleManager.FindByIdAsync(role.Id);
            if (identityRole == null)
            {
                return NotFound();
            }
            identityRole.Name = role.Name;
            await roleManager.UpdateAsync(identityRole);
            return Ok(identityRole);
        }

        [HttpPut]
        [Route("Updateroles")]
        public async Task<ActionResult> updateRoles(string userId, List<string> roles)
        {

            if (userId == null || roles == null)
            {
                return BadRequest("roles or userId is null");
            }
            AppUser? user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));
            await userManager.AddToRolesAsync(user, roles);
            return Ok();
        }
        [HttpGet]
        [Route("AllRole")]
        public ActionResult getAllRoles()
        {
            if (roleManager.Roles.ToList() == null) return NotFound();
            else return Ok(roleManager.Roles.ToList());
        }

        [HttpDelete]
        [Route("DeleteRole")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            if (await roleManager.FindByIdAsync(id) == null) return NotFound();
            else
            {
                await roleManager.DeleteAsync(await roleManager.FindByIdAsync(id));
                return Ok();
            }
        }


        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (await userManager.FindByIdAsync(id) == null) return NotFound();
            else
            {
                await userManager.DeleteAsync(await userManager.FindByIdAsync(id));
                return Ok();
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult> GetUser(string id)
        {
            if (await userManager.FindByIdAsync(id) == null) return NotFound();
            else
            {
                return Ok(await userManager.FindByIdAsync(id));
            }
        }
        [HttpGet]
        [Route("AllUsers")]
        public ActionResult getAllUsers()
        {
            if (userManager.Users.ToList() == null) return NotFound();
            else return Ok(userManager.Users.ToList());
        }

        [HttpGet]
        [Route("getRoleUsers")]
        public async Task<ActionResult> getRoleUsers(string role)
        {
            if (role == null) return BadRequest("role is null");
            if (await userManager.GetUsersInRoleAsync(role) == null)
                return NotFound();
            else return Ok(await userManager.GetUsersInRoleAsync(role));
        }

    }
}
