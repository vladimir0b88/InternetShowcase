using Application.Common;
using Application.Models;
using Domain.Constants;
using Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<Created>> Register([FromBody] UserRegisterDto registerDto)
        {
            var result = await userService.Register(registerDto);

            return this.SendResponse(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto loginDto)
        {
            var result = await userService.Login(loginDto);

            if (!result.IsError)
                Response.Cookies.Append("jwt-token", result.Value);

            return this.SendResponse(result);
        }


        [Authorize(Roles = Roles.Administrator)]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var result = await userService.GetAllUsers();

            return this.SendResponse(result);
        }

    }
}
