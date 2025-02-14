using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Models.CreateUser;
using Kanboom.Models.CreateUser.DTO;
using Kanboom.Models.RetrieveUserInfo.DTO;
using Kanboom.Models.RetrieveUserInfo;
using Kanboom.Utils; 


namespace Kanboom.Controllers;

public class UserController : ControllerBase 
{
    private readonly IUserService _userService;
    private readonly IUserInfoService _userInfoService;

    public UserController(IUserService userService, IUserInfoService userInfoService){
        _userService = userService;
        _userInfoService = userInfoService;

    }
    
    [Verification]
    [HttpPost("user")]
    public async Task<ActionResult<RetrieveUserInfoResponse>> RetrieveUserInfo([FromBody] RetrieveUserInfoRequest request){
        try {
            var user = new RetrieveUserInfoRequestDTO{
                Token = request.Token
            };

            var response = await _userInfoService.RetrieveInfo(user);

            if(!response.IsSuccessful){
                return BadRequest(RetrieveUserInfoResponse.FromFailure(response.Message));
            }

            return Ok(RetrieveUserInfoResponse.FromSuccess(response.Username, response.ProfilePic, response.Boards, response.Tasks, response.Id));
        }
        catch(Exception e){
            return StatusCode(500, RetrieveUserInfoResponse.FromError(e.Message));
        }
    }

    [Verification]
    [HttpPost("user/create")]
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
