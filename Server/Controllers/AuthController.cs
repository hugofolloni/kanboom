using Kanboom.Models.AuthUser;
using Kanboom.Models.AuthUser.DTO;
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
        public async Task<ActionResult<AuthUserResponse>> Login([FromBody] AuthUserRequest request){
            try {
                var user = new AuthUserRequestDTO{
                    Username = request.Username,
                    Password = request.PasswordHash
                };

                var response = await _authService.CheckLogin(user);

                if(!response.IsSuccessful){
                    return BadRequest(AuthUserResponse.FromFailure(response.Message));
                }

                return Ok(AuthUserResponse.FromSuccess(response.Token));
       
                    
            }
            catch(Exception e){
                return StatusCode(500, e.Message);
            }
        }
        
    }
}