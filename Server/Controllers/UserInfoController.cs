using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Models.RetrieveUserInfo.DTO;
using Kanboom.Models.RetrieveUserInfo;
using Kanboom.Utils; 

namespace Kanboom.Controllers;

public class UserInfoController : ControllerBase 
{
    private readonly IUserInfoService _userInfoService;

    public UserInfoController(IUserInfoService userInfoService){
        _userInfoService = userInfoService;
    }
    
    [Verification]
    [HttpPost("userInfo")]
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


}
