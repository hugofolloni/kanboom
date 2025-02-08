using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Utils; 
using Kanboom.Models.CreateUser;
using Kanboom.Models.CreateUser.DTO;

namespace Kanboom.Controllers;

public class UserController : ControllerBase 
{
    private readonly IUserService _userService;

    public UserController(IUserService userService){
        _userService = userService;
    }

    // <summary>
    // Retrieves users from database
    // </summary>
    [Verification]
    [HttpPost("user")]
    public async Task<ActionResult<CreateUserResponse>> Login([FromBody] CreateUserRequest request){
        try {
            var user = new CreateUserRequestDTO{
                Username = request.Username,
                PasswordHash = request.PasswordHash,
                Email = request.Email
            };

            var response = await _userService.CreateUser(user);

            if(!response.IsSuccessful){
                return BadRequest(CreateUserResponse.FromFailure(response.Message));
            }

            return Ok(CreateUserResponse.FromSuccess(response.Username, response.Token));

                
        }
        catch(Exception e){
            return StatusCode(500, CreateUserResponse.FromError(e.Message));
        }
    }


}
