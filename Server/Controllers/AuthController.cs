using Kanboom.Models;
using Kanboom.Models.DTO;
using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kanboom.Controllers {
    public class AuthController : ControllerBase 
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService){
            _authService = authService;
        }

        // <summary>
        // Retrieves users from database
        // </summary>
        [HttpPost("auth/login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] UserRequest request){
            try {
                var user = new UserRequestDTO{
                    Username = request.Username,
                    Password = request.PasswordHash
                };

                var response = await _authService.CheckLogin(user);

                if(!response.IsSuccessful){
                    return BadRequest(UserResponse.FromFailure(response.Message));
                }

                return Ok(UserResponse.FromSuccess(response.Token));
            }
            catch(Exception e){
                return StatusCode(500, e.Message);
            }
        }
        
    }
}