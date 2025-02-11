using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Models.CreateBoard;
using Kanboom.Models.CreateBoard.DTO;
using Kanboom.Models.RetrieveBoard.DTO;
using Kanboom.Models.RetrieveBoard;
using Kanboom.Utils; 

namespace Kanboom.Controllers;

public class BoardController : ControllerBase 
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService){
        _boardService = boardService;
    }
    
    [Verification]
    [HttpPost("board/create")]
    public async Task<ActionResult<CreateBoardResponse>> CreateBoard([FromBody] CreateBoardRequest request){
        try {
            var user = new CreateBoardRequestDTO{
                Name = request.Name,
                Token = request.Token
            };

            var response = await _boardService.CreateBoard(user);

            if(!response.IsSuccessful){
                return BadRequest(CreateBoardResponse.FromFailure(response.Message));
            }

            return Ok(CreateBoardResponse.FromSuccess(response.Board));

                
        }
        catch(Exception e){
            return StatusCode(500, CreateBoardResponse.FromError(e.Message));
        }
    }
    
    [Verification]
    [HttpPost("board")]
    public async Task<ActionResult<RetrieveBoardResponse>> GetBoard([FromBody] RetrieveBoardRequest request){
        try {
            var board = new RetrieveBoardRequestDTO{
                BoardId = request.BoardId,
                Token = request.Token
            };

            var response = await _boardService.RetrieveBoard(board);

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
    [HttpPost("boardUser/invite")]
    public async Task<ActionResult<RetrieveBoardResponse>> AddUserToBoard([FromBody] HandleInviteRequest request){
        try {
            var boardUser = new HandleInviteRequestDTO{
                Invite = request.Invite,
                Token = request.Token
            };

            var response = await _boardService.AddUserToBoard(boardUser);

            if(!response.IsSuccessful){
                return BadRequest(RetrieveBoardResponse.FromFailure("USER_COULDNT_BE_ADDED"));
            }

            return Ok(RetrieveBoardResponse.FromSuccess(response.Board));

                
        }
        catch(Exception e){
            return StatusCode(500, RetrieveBoardResponse.FromError(e.Message));
        }
    }

    

}
