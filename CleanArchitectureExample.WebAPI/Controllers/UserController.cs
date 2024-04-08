using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public ActionResult<UserDto> GetUserByEmail(string email)
        {
            var userDto = _userService.GetUserByEmail(email);
            if (userDto == null)
            {
                return NotFound();
            }
            return userDto;
        }
    }
}
