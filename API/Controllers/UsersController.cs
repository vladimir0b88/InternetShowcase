using Application.Common;
using Application.Models;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController (IUserService userService): ControllerBase
    {
        [Authorize(Roles = Roles.Administrator)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await userService.GetAllUsers();

            return result switch
            {
                SuccessResult<List<User>> => Ok(result),
                ErrorResult<List<User>> => BadRequest(result),
                _ => throw new ApplicationException()
            };
        }

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
