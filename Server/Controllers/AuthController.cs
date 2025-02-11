using Kanboom.Models.AuthLogin;
using Kanboom.Models.AuthLogin.DTO;
using Kanboom.Models.PersistUser;
using Kanboom.Models.PersistUser.DTO;
using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Utils; 

namespace Kanboom.Controllers;

public class AuthController : ControllerBase 
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService){
        _authService = authService;
    }

    [Verification]
    [HttpPost("auth/login")]
    public async Task<ActionResult<AuthLoginResponse>> Login([FromBody] AuthLoginRequest request){
        try {
            var user = new AuthLoginRequestDTO{
                Username = request.Username,
                Password = request.PasswordHash
            };

            var response = await _authService.CheckLogin(user);

            if(!response.IsSuccessful){
                return BadRequest(AuthLoginResponse.FromFailure(response.Message));
            }

            return Ok(AuthLoginResponse.FromSuccess(response.Token));

                
        }
        catch(Exception e){
            return StatusCode(500, AuthLoginResponse.FromError(e.Message));
        }
    }

    [Verification]
    [HttpPost("auth/persist")]
    public async Task<ActionResult<PersistUserResponse>> Persist([FromBody] PersistUserRequest request)
    {
        try {
            var token = new PersistUserRequestDTO{
                Token = request.Token,
            };

            var response = _authService.ValidateToken(token);

            if(!response.IsSuccessful){
                return BadRequest(PersistUserResponse.FromFailure(response.Message));
            }

            return Ok(PersistUserResponse.FromSuccess(response.Username));
            
        }
        catch(Exception e){
            return StatusCode(500, PersistUserResponse.FromError(e.Message));
        }
    }
}
