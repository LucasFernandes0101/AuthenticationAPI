using AuthenticationAPI.Application.Interfaces;
using AuthenticationAPI.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserViewModel user)
        {
            var token = await _userService.CreateAsync(user);

            return Ok(token);
        }

        [HttpPost]
        [Route("ValidateUserPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateUserPasswordAsync(Guid idUser, string password)
        {
            var token = await _userService.ValidateUserPasswordAsync(idUser, password);

            return Ok(token);
        }
    }
}
