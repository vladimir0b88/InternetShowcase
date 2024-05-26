using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.Users.Login;
using Application.Models.Users.Register;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController (IUserService userService): ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            var result = await userService.Register(registerDto);

            return result switch
            {
                SuccessResult => Created(),
                ValidationErrorResult => StatusCode(422, result),
                ErrorResult => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var result = await userService.Login(loginDto);

            if (result is SuccessResult<string> successResult)
                Response.Cookies.Append("jwt-token", successResult.Data);
            
            return result switch
            {
                SuccessResult<string> => Ok(result),
                ValidationErrorResult<string> => StatusCode(422, result),
                ErrorResult<string> => BadRequest(result),
                _ => throw new ApplicationException()
            }; ;
        }
    }
}
