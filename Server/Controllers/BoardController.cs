using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Models.CreateBoard;
using Kanboom.Models.CreateBoard.DTO;
using Kanboom.Models.RetrieveBoard.DTO;
using Kanboom.Models.RetrieveBoard;
using Kanboom.Utils;
using Kanboom.Models.ChangeBoardOwner;
using Kanboom.Models.ChangeBoardOwner.DTO;

namespace Kanboom.Controllers;

public class BoardController : ControllerBase 
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService){
        _boardService = boardService;
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
    [HttpPatch("board/changeOwner")]
    public async Task<ActionResult<ChangeBoardOwnerResponse>> ChangeOwner([FromBody] ChangeBoardOwnerRequest request){
        try {
            var boardUser = new ChangeBoardOwnerRequestDTO{
                BoardId = request.BoardId,
                Token = request.Token,
                BoardOwner = request.Owner
            };

            var response = await _boardService.ChangeOwner(boardUser);

            if(!response.IsSuccessful){
                return BadRequest(ChangeBoardOwnerResponse.FromFailure(response.Message));
            }

            return Ok(ChangeBoardOwnerResponse.FromSuccess(response.Board));

                
        }
        catch(Exception e){
            return StatusCode(500, ChangeBoardOwnerResponse.FromError(e.Message));
        }
    }
}
