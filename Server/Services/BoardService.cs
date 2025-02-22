using Kanboom.Services.Interfaces;
using Kanboom.Repositories.Interfaces;
using Kanboom.Models;
using Kanboom.Models.Database;
using Kanboom.Models.CreateBoard.DTO;
using Kanboom.Models.RetrieveBoard.DTO;
using Kanboom.Models.LeaveBoard.DTO;
using Kanboom.Models.ChangeBoardOwner.DTO;
using Kanboom.Models.ChangeBoardStages.DTO;
using Kanboom.Models.ChangeTaskStage.DTO;
using Kanboom.Models.ChangeTaskVisibility.DTO;

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

    public async Task<RetrieveBoardResponseDTO> RetrieveBoard(RetrieveBoardRequestDTO request){
        var response = new RetrieveBoardResponseDTO();
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

    public async Task<RetrieveBoardResponseDTO> AddUserToBoard(HandleInviteRequestDTO request)
    {
        var response = new RetrieveBoardResponseDTO();
        try{
            var boardId = await _repository.RetrieveBoardIdByInvite(request.Invite);
            var userId = await _userService.GetUserIdByToken(request.Token);

            if (boardId == null){
                response.IsSuccessful = false;
                response.Message = "INVITE_NOT_FOUND";
                return response;
            }

            var boardUsers = await _userService.GetBoardUsers(boardId);

            if(boardUsers.Contains(userId)){
                response.IsSuccessful = false;
                response.Message = "USER_ALREADY_IN_BOARD";
                return response;
            }

            var data = await _repository.AddUserToBoard(userId, boardId);

            if (!data){
                response.IsSuccessful = false;
                response.Message = "COULDNT_ADD_USER_TO_BOARD";
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

    public async Task<LeaveBoardResponseDTO> LeaveBoard(LeaveBoardRequestDTO request){
        var response = new LeaveBoardResponseDTO();

        try{
            var userId = await _userService.GetUserIdByToken(request.Token);

            if(!await _repository.CheckUser(userId, request.BoardId)){
                response.IsSuccessful = false;
                response.Message = "USER_NOT_IN_BOARD";
                return response;
            }

            var boardInfo = await _repository.RetrieveBoard(request.BoardId);

            if(boardInfo.BoardUser.Count == 1 && boardInfo.Fk_BoardOwner.Equals(userId)){
                await _repository.DeleteBoard(request.BoardId);
                response.IsSuccessful = true;  
                return response; // Already deleted user from board, so it can return 
            }

            if(boardInfo.Fk_BoardOwner.Equals(userId)){
                var secondUser = boardInfo.BoardUser.ElementAtOrDefault(1);
                await _repository.ChangeOwner(secondUser.Id, request.BoardId);
            }

            var tasksAssignee = await _repository.GetTasksByUserInBoard(userId, boardInfo.Id);
            foreach(Models.Database.Task task in tasksAssignee){
                await _taskService.HandleTaskOwnerLeavingGroup(task.Id, boardInfo.Fk_BoardOwner);
            }

            var data = await _repository.RemoveUserFromBoard(userId, request.BoardId);

            if (!data){
                response.IsSuccessful = false;
                response.Message = "COULDNT_LEAVE_BOARD";
                return response;
            }

            response.IsSuccessful = true;
            return response;
        }
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
        
    }

    public async Task<ChangeBoardOwnerResponseDTO> ChangeOwner(ChangeBoardOwnerRequestDTO request)
    {
        var response = new ChangeBoardOwnerResponseDTO();
        try{
            var boardUsers = await _userService.GetBoardUsers(request.BoardId);
            var editorId = await _userService.GetUserIdByToken(request.Token);
            
            var boardInfo = await _repository.RetrieveBoard(request.BoardId);

            if(!boardInfo.Fk_BoardOwner.Equals(editorId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CHANGE_OWNER";
                return response;
            }

            if(!boardUsers.Contains(request.BoardOwner)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_BE_ASSIGNEE_AS_OWNER";
                return response;    
            }

            var data = await _repository.ChangeOwner(request.BoardOwner, request.BoardId);

            response.Board = new Domain.Board{Id = data.Id, Name = data.Name, StagesCount = data.StagesCount, Fk_BoardOwner = data.Fk_BoardOwner, IsGroupBoard = data.IsGroupBoard, Tasks = await _taskService.GetTasksInBoard(data.Id), Users = await _userService.GetBoardUsers(data.Id), StageLevels = await RetrieveLabelsForBoardLevels(data.Id), Invite = data.Invite};
            response.IsSuccessful = true;
            return response;
        }
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ChangeBoardStagesResponseDTO> AddStageToBoard(ChangeBoardStagesRequestDTO request){
        var response = new ChangeBoardStagesResponseDTO();
        try { 
            var editorId = await _userService.GetUserIdByToken(request.Token);
            var boardBasicData = await _repository.RetrieveBoard(request.BoardId);
            var board = new Domain.Board{Id = boardBasicData.Id, Name = boardBasicData.Name, StagesCount = boardBasicData.StagesCount, Fk_BoardOwner = boardBasicData.Fk_BoardOwner, IsGroupBoard = boardBasicData.IsGroupBoard, Tasks = await _taskService.GetTasksInBoard(boardBasicData.Id), Users = await _userService.GetBoardUsers(boardBasicData.Id), StageLevels = await RetrieveLabelsForBoardLevels(boardBasicData.Id), Invite = boardBasicData.Invite};

             if(!board.Fk_BoardOwner.Equals(editorId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CHANGE_BOARD_STAGES";
                return response;
            }

            var currentStages = board.StageLevels.Where(s => s.StageNumber >= request.StageNumber).OrderBy(s => s.StageNumber).ToList();

            foreach(Domain.StageLevel stage in currentStages){
                await _repository.UpdateStageNumber(board.Id, stage.StageNumber, stage.StageNumber + 1);
                var taskInStage = board.Tasks.Where(t => t.StageNumber == stage.StageNumber).ToList();
                foreach(Domain.Task task in taskInStage){
                    var taskDTO = new ChangeTaskStageRequestDTO{
                        Id = task.Id,
                        Stage = stage.StageNumber + 1,
                        Token = request.Token,
                        Fk_Board = request.BoardId
                    };
                    await _taskService.ChangeStage(taskDTO);
                }
            }

            var data = await _repository.AddStageToBoard(request);

            if (!data){
                response.IsSuccessful = false;
                response.Message = "COULDNT_ADD_STAGE_TO_BOARD";
                return response;
            }

            response.Board = new Domain.Board{Id = board.Id, Name = board.Name, StagesCount = board.StagesCount + 1, Fk_BoardOwner = board.Fk_BoardOwner, IsGroupBoard = board.IsGroupBoard, Tasks = await _taskService.GetTasksInBoard(board.Id), Users = await _userService.GetBoardUsers(board.Id), StageLevels = await RetrieveLabelsForBoardLevels(board.Id), Invite = board.Invite};
            response.IsSuccessful = true;
            return response;
        }
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
    }
    
    public async Task<ChangeBoardStagesResponseDTO> RemoveStageFromBoard(ChangeBoardStagesRequestDTO request){
        var response = new ChangeBoardStagesResponseDTO();
        try {
            var editorId = await _userService.GetUserIdByToken(request.Token);
            var boardBasicData = await _repository.RetrieveBoard(request.BoardId);
            var board = new Domain.Board{Id = boardBasicData.Id, Name = boardBasicData.Name, StagesCount = boardBasicData.StagesCount, Fk_BoardOwner = boardBasicData.Fk_BoardOwner, IsGroupBoard = boardBasicData.IsGroupBoard, Tasks = await _taskService.GetTasksInBoard(boardBasicData.Id), Users = await _userService.GetBoardUsers(boardBasicData.Id), StageLevels = await RetrieveLabelsForBoardLevels(boardBasicData.Id), Invite = boardBasicData.Invite};

             if(!board.Fk_BoardOwner.Equals(editorId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CHANGE_BOARD_STAGES";
                return response;
            }

            var stageToBeDeleted = board.StageLevels.Where(s => s.StageNumber == request.StageNumber).FirstOrDefault();
            if(stageToBeDeleted == null){
                response.IsSuccessful = false;
                response.Message = "STAGE_NOT_FOUND";
                return response;
            }

            if(request.StageNumber == 0){
                var zeroTasks = board.Tasks.Where(t => t.StageNumber == 0).ToList();
                foreach(Domain.Task task in zeroTasks){
                    var changeVisibilityDTO = new ChangeTaskVisibilityRequestDTO{
                        Id = task.Id,
                        Token = request.Token,
                        Hidden = true,
                        Fk_Board = request.BoardId
                    };
                    await _taskService.ChangeVisibility(changeVisibilityDTO);
                }
            }

            var affectedTasks = board.Tasks.Where(t => t.StageNumber >= stageToBeDeleted.StageNumber).ToList();
            foreach(Domain.Task task in affectedTasks){
                if(task.StageNumber == 0){
                    continue;
                }
                var taskDTO = new ChangeTaskStageRequestDTO{
                    Id = task.Id,
                    Stage = (int) task.StageNumber - 1,
                    Token = request.Token,
                    Fk_Board = request.BoardId
                };
                await _taskService.ChangeStage(taskDTO);
            }

            var data = await _repository.RemoveStageFromBoard(request);
            if (!data){
                response.IsSuccessful = false;
                response.Message = "COULDNT_REMOVE_STAGE_FROM_BOARD";
                return response;
            }

            var currentStages = board.StageLevels.Where(s => s.StageNumber > request.StageNumber).OrderBy(s => s.StageNumber).ToList();

            foreach(Domain.StageLevel stage in currentStages){
                await _repository.UpdateStageNumber(board.Id, stage.StageNumber, stage.StageNumber - 1);
            }

            response.Board = new Domain.Board{Id = board.Id, Name = board.Name, StagesCount = board.StagesCount - 1, Fk_BoardOwner = board.Fk_BoardOwner, IsGroupBoard = board.IsGroupBoard, Tasks = await _taskService.GetTasksInBoard(board.Id), Users = await _userService.GetBoardUsers(board.Id), StageLevels = await RetrieveLabelsForBoardLevels(board.Id), Invite = board.Invite};
            response.IsSuccessful = true;
            return response;
        } 
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.ToString();
            return response;
        }
    }

    public async Task<ChangeBoardStagesResponseDTO> RenameStage(ChangeBoardStagesRequestDTO request){
        var response = new ChangeBoardStagesResponseDTO();
        try {
            var editorId = await _userService.GetUserIdByToken(request.Token);
            var boardBasicData = await _repository.RetrieveBoard(request.BoardId);
            var board = new Domain.Board{Id = boardBasicData.Id, Name = boardBasicData.Name, StagesCount = boardBasicData.StagesCount, Fk_BoardOwner = boardBasicData.Fk_BoardOwner, IsGroupBoard = boardBasicData.IsGroupBoard, Tasks = await _taskService.GetTasksInBoard(boardBasicData.Id), Users = await _userService.GetBoardUsers(boardBasicData.Id), StageLevels = await RetrieveLabelsForBoardLevels(boardBasicData.Id), Invite = boardBasicData.Invite};

             if(!board.Fk_BoardOwner.Equals(editorId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CHANGE_BOARD_STAGES";
                return response;
            }

            var data = await _repository.RenameStage(request);

            response.Board = new Domain.Board{Id = board.Id, Name = board.Name, StagesCount = board.StagesCount + 1, Fk_BoardOwner = board.Fk_BoardOwner, IsGroupBoard = board.IsGroupBoard, Tasks = await _taskService.GetTasksInBoard(board.Id), Users = await _userService.GetBoardUsers(board.Id), StageLevels = await RetrieveLabelsForBoardLevels(board.Id), Invite = board.Invite};
            response.IsSuccessful = true;
            return response;
        } 
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
    }

}