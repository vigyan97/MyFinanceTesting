using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models.DtoModels;

namespace MyFinance.Controllers.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserRole(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email is required.");
            var user = await _userManager.FindByEmailAsync(email);
            IList<string> roles = new List<string>();
            if (user != null)
            {
               roles= await _userManager.GetRolesAsync(user);
            }
            else
            {
                return BadRequest($"Email '{email}' does not exist.");
            }
            return Ok(roles);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name is required.");

            if (await _roleManager.RoleExistsAsync(roleName))
                return Conflict($"Role '{roleName}' already exists.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
                return Ok($"Role '{roleName}' created successfully.");

            return BadRequest(result.Errors);
        }

        [HttpPost("addUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToRole([FromBody] UserToRole userToRole)
        {
            if (string.IsNullOrWhiteSpace(userToRole.Email))
                return BadRequest("Email is required.");
            if (string.IsNullOrWhiteSpace(userToRole.Role))
                return BadRequest("Role is required.");

            if (!await _roleManager.RoleExistsAsync(userToRole.Role))
                return BadRequest($"Role '{userToRole.Role}' does not exist.");

            var user = await _userManager.FindByEmailAsync(userToRole.Email);

            if ( user == null)
                return BadRequest($"Email '{userToRole.Email}' does not exist.");


            var result = await _userManager.AddToRoleAsync(user, userToRole.Role);

            if (result.Succeeded)
                return Ok($"Role '{userToRole.Role}' assigned to '{userToRole.Email}' successfully.");

            return BadRequest(result.Errors);
        }
    }

}
