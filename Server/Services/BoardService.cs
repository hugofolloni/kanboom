using Kanboom.Services.Interfaces;
using Kanboom.Repositories.Interfaces;
using Kanboom.Models.CreateBoard.DTO;
using Kanboom.Models;
using Kanboom.Models.RetrieveBoard.DTO;
using Kanboom.Models.Database;

namespace Kanboom.Services;
 
public class BoardService : IBoardService
 {
    private readonly IBoardRepository _repository;
    private readonly ITaskService _taskService;
    private readonly IUserService _userService;

    public BoardService(IBoardRepository repository, ITaskService taskService, IUserService userService){
        _repository = repository;
        _taskService = taskService;
        _userService = userService;
    }

    public async Task<List<Domain.Board>> GetBoardsByUser(long userId){
        var response = new List<Domain.Board>();
        
        var boardInfo = await _repository.GetBoardsByUser(userId);
        
        foreach (Board board in boardInfo)
        {
            var userBoard = new Domain.Board{Id = board.Id, Name = board.Name, StagesCount = board.StagesCount, Fk_BoardOwner = board.Fk_BoardOwner, IsGroupBoard = board.IsGroupBoard, Invite = board.Invite};
            userBoard.IncompletedTasks = await _taskService.GetIncompletedTaskNumberByBoard(board);
            response.Add(userBoard);
        }

        return response;
    }

    public async Task<CreateBoardResponseDTO> CreateBoard(CreateBoardRequestDTO request) {
        var response = new CreateBoardResponseDTO();
        try{
            var userId = await _userService.GetUserIdByToken(request.Token);

            var data = await _repository.CreateBoard(request, userId);

            if (data == null){
                response.IsSuccessful = false;
                response.Message = "COULDNT_CREATE_BOARD";
                return response;
            }

            response.IsSuccessful = true;
            response.Board = new Domain.Board{Id = data.Id, Name = data.Name, StagesCount = data.StagesCount, Fk_BoardOwner = data.Fk_BoardOwner, IsGroupBoard = data.IsGroupBoard, Invite = data.Invite};
                    
            return response;
        }
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<RetrieveBoardReponseDTO> RetrieveBoard(RetrieveBoardRequestDTO request){
        var response = new RetrieveBoardReponseDTO();
        try {
            if(!await _repository.CheckUser(await _userService.GetUserIdByToken(request.Token), request.BoardId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANNOT_ACCESS_BOARD";
                return response;
            }

            var data = await _repository.RetrieveBoard(request.BoardId);

            if (data == null){
                response.IsSuccessful = false;
                response.Message = "COULDNT_RETRIEVE_BOARD";
                return response;
            }

            response.Board = new Domain.Board{Id = data.Id, Name = data.Name, StagesCount = data.StagesCount, Fk_BoardOwner = data.Fk_BoardOwner, IsGroupBoard = data.IsGroupBoard, Tasks = await _taskService.GetTasksInBoard(data.Id), Users = await _userService.GetBoardUsers(data.Id), StageLevels = await RetrieveLabelsForBoardLevels(data.Id), Invite = data.Invite};
            response.IsSuccessful = true;
            return response;
        }
        catch(Exception ex) {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
    }

    private async Task<List<Domain.StageLevel>> RetrieveLabelsForBoardLevels(long? boardId){
        var response = new List<Domain.StageLevel>();
        
        var data = await _repository.RetrieveLabels(boardId);
        foreach(StageLevels level in data){
            response.Add(new Domain.StageLevel{StageName = level.StageName, StageNumber = level.StageNumber});
        }

        return response;
    }

    public async Task<RetrieveBoardReponseDTO> AddUserToBoard(HandleInviteRequestDTO request)
    {
        var response = new RetrieveBoardReponseDTO();
        try{
            var boardId = await _repository.RetrieveBoardIdByInvite(request.Invite);

            if (boardId == null){
                response.IsSuccessful = false;
                response.Message = "INVITE_NOT_FOUND";
                return response;
            }

            var data = await _repository.AddUserToBoard(await _userService.GetUserIdByToken(request.Token), boardId);

            if (!data){
                response.IsSuccessful = false;
                response.Message = "COULDNT_ADD_USER";
                return response;
            }

            return await RetrieveBoard(new RetrieveBoardRequestDTO{Token = request.Token, BoardId = boardId});
        }
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
    }


}