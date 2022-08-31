using Azure;
using Internship_2022.Application.Interfaces;
using Internship_2022.Application.JwtUtil;
using Internship_2022.Application.Models.UserDto;
using Internship_2022.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Internship_2022_august_be1.Controllers
{

    [Authorize]
    [Route("api/[controller]"), ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost, Route("/api/user/authenticate")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel login)
        {
            var response = await userService.Authenticate(login);

            if (response == null)
                return Unauthorized();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost, Route("/api/user/register")]
        public async Task<IActionResult> Register(CreateUserRequestDto user)
        {
            try
            {
                await userService.Register(user);
                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("/api/user/reset-password")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                await userService.ResetPassword(email);

                return Ok("Password reset with success");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPut, Route("/api/user/{id}")]
        public async Task<IActionResult> UpdateUserById(Guid id, UpdateRequestDto user)
        {
            try
           {
                await userService.UpdateUserById(id, user);
                return Ok();
           }
            catch(Exception ex)
           {
               logger.LogError(ex.Message);

               return BadRequest();
          }
        }

        [HttpPatch("patch/{userId}")]
        public async Task<ActionResult> UpdateUserByProperty([FromBody]dynamic test, Guid userId)
        {
            try
            {
                await userService.PatchUserByIdAsync(test, userId);

                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
            await userService.PatchUserByIdAsync(test, userId);
            return Ok();
        }
    

        [AuthorizeMultiplePolicy(Policies.Admin, true)]
        [HttpGet, Route("/api/user/{id}")]
        public async Task<ActionResult<UserRequestDto>> GetUserById(Guid id)
        {
            try
            {
                var result = await userService.GetUserById(id);

                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }
        [AuthorizeMultiplePolicy(Policies.Admin, false)]
        [HttpGet, Route("/api/user")]
        public async Task<ActionResult<IReadOnlyList<User>>> GetAllUsers()
        {
            try
            {
                var result = await userService.GetAllUsers();

                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpDelete, Route("/api/user/{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {

                await userService.DeleteUserById(id);

                return Ok();

              
        }
    }
}
