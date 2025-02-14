using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Models.RetrieveBoard.DTO;
using Kanboom.Models.RetrieveBoard;
using Kanboom.Utils;
using Kanboom.Models.LeaveBoard;
using Kanboom.Models.LeaveBoard.DTO;

namespace Kanboom.Controllers;

public class BoardUserController : ControllerBase 
{
    private readonly IBoardService _boardService;
    public BoardUserController(IBoardService boardService){
        _boardService = boardService;
    }

    [Verification]
    [HttpPost("boardUser/invite")]
    public async Task<ActionResult<RetrieveBoardResponse>> AddUserToBoard([FromBody] HandleInviteRequest request){
        try {
            var boardUser = new HandleInviteRequestDTO{
                Invite = request.Invite,
                Token = request.Token
            };

            var response = await _boardService.AddUserToBoard(boardUser);

            if(!response.IsSuccessful){
                return BadRequest(RetrieveBoardResponse.FromFailure(response.Message));
            }

            return Ok(RetrieveBoardResponse.FromSuccess(response.Board));

                
        }
        catch(Exception e){
            return StatusCode(500, RetrieveBoardResponse.FromError(e.Message));
        }
    }

    [Verification]
    [HttpDelete("boardUser/leave")]
    public async Task<ActionResult<LeaveBoardResponse>> LeaveBoard([FromBody] LeaveBoardRequest request){
        try {
            var boardUser = new LeaveBoardRequestDTO{
                BoardId = request.BoardId,
                Token = request.Token
            };

            var response = await _boardService.LeaveBoard(boardUser);

            if(!response.IsSuccessful){
                return BadRequest(LeaveBoardResponse.FromFailure("USER_COULDNT_BE_REMOVED"));
            }

            return Ok(LeaveBoardResponse.FromSuccess());

                
        }
        catch(Exception e){
            return StatusCode(500, LeaveBoardResponse.FromError(e.Message));
        }
    }

}