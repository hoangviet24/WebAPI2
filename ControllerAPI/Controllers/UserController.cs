using ControllerAPI.Repository.Auth;
using DataAnimals.DTO.Login;
using DataAnimals.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public UserController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        [HttpGet]
        [Route("ListUsers"),Authorize(Roles = "Write")]
        public async Task<IActionResult> ListUsers()
        {
            var users = _userManager.Users.ToList();
            var userList = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserDTO
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return Ok(userList);
        }
        [HttpGet]
        [Route("GetUser/{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            // Find the user by username
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Get roles for this user
            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new UserDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Roles = roles.ToList()
            };

            return Ok(userDto);
        }
        [HttpDelete]
        [Route("DeleteUser/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            // Find the user by username
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Delete the user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to delete user");
            }

            return Ok("User deleted successfully");
        }
        [HttpPost]
        [Route("UpdateRoles")]
        public async Task<IActionResult> UpdateRoles([FromBody] UpdateUserRolesDTO updateUserRolesDTO)
        {
            // Find the user by username
            var user = await _userManager.FindByNameAsync(updateUserRolesDTO.Username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Get current roles
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove existing roles
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return BadRequest("Failed to remove existing roles");
            }

            // Add new roles
            var addResult = await _userManager.AddToRolesAsync(user, updateUserRolesDTO.Roles);
            if (!addResult.Succeeded)
            {
                return BadRequest("Failed to add new roles");
            }

            return Ok("Roles updated successfully");
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };
            var identityResult = await _userManager.CreateAsync(identityUser,
           registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                //add roles to this user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser,
                   registerRequestDTO.Roles);
                }
                if (identityResult.Succeeded)
                {
                    return Ok("Register Successful! Let login!");
                }
            }
            return BadRequest("Something wrong!");
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (user != null)
            {
                var checkPasswordResult = await
               _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);// kiểm tra password nhập vào khớp với password lưu ở database
                if (checkPasswordResult)
                {
                    //get roles for this user – lấy quyền của user từ database
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //create token – tạo token cho user này
                        var jwtToken = _tokenRepository.CreateJWTToken(user,
                       roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response); // trả về chuỗi token
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
