using Kanboom.Models.Database;
using Kanboom.Services.Interfaces;
using Kanboom.Repositories.Interfaces;
using Kanboom.Models;
using Kanboom.Models.CreateTask.DTO;
using Kanboom.Models.EditTask.DTO;
using Kanboom.Models.ChangeTaskVisibility.DTO;
using Kanboom.Models.ChangeTaskStage.DTO;
using Kanboom.Models.ChangeTaskAssignee.DTO;

namespace Kanboom.Services;
 
public class TaskService : ITaskService {
    private readonly ITaskRepository _repository;
    private readonly IUserService _userService;

    public TaskService(ITaskRepository repository, IUserService userService){
        _repository = repository;
        _userService = userService;
    }

    public async Task<int> GetIncompletedTaskNumberByBoard(Board board) {
        var tasksInBoard = await _repository.GetTasksByBoard(board.Id);
        var finalStage = board.StagesCount - 1;
        
        var uncompleted = 0;

        foreach (Models.Database.Task task in tasksInBoard)
        {
            if(task.StageNumber < finalStage){
                uncompleted++;
            }
        }

        return uncompleted;
    }

    public async Task<List<Domain.Task>> GetTasksInBoard(long boardId){
        var response = new List<Domain.Task>();
        
        var boardTasks = await _repository.GetTasksByBoard(boardId);
        
        foreach (Models.Database.Task task in boardTasks)
        {
            var boardTask = new Domain.Task{Id = task.Id, Title = task.Title, Description = task.Description, StageNumber = task.StageNumber, Fk_UserAssignee = task.Fk_UserAssignee, Fk_Board = task.Fk_Board, Hidden = task.Hidden};
            response.Add(boardTask);
        }

        return response;
    }

    public async Task<CreateTaskResponseDTO> CreateTask(CreateTaskRequestDTO request)
    {
        var response = new CreateTaskResponseDTO();
        try{
            var boardUsers = await _userService.GetBoardUsers(request.Fk_Board);
            var creatorId = await _userService.GetUserIdByToken(request.Token);

            if(!boardUsers.Contains(creatorId) || (!boardUsers.Contains(request.Fk_UserAssignee) && request.Fk_UserAssignee != null)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CREATE_OR_BE_ASSIGNEE_TO_TASK";
                return response;    
            }

            var data = await _repository.CreateTask(request);

            response.Task = TransformDataInDomain(data);
            response.IsSuccessful = true;

            return response;
        }
        catch(Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            response.Task = null;
            return response;
        }
    }
    
    public async Task<EditTaskResponseDTO> EditTask(EditTaskRequestDTO request)
    {   
        var response = new EditTaskResponseDTO();
        try{
            var boardUsers = await _userService.GetBoardUsers(request.Fk_Board);
            var editorId = await _userService.GetUserIdByToken(request.Token);

            if(!boardUsers.Contains(editorId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_EDIT_TASK";
                return response;    
            }

            var data = await _repository.EditTask(request);

            response.Task = TransformDataInDomain(data);
            response.IsSuccessful = true;

            return response;
        }
        catch(Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            response.Task = null;
            return response;
        }
    }

    public async Task<ChangeTaskVisibilityResponseDTO> ChangeVisibility(ChangeTaskVisibilityRequestDTO request)
    {
        var response = new ChangeTaskVisibilityResponseDTO();   
        try{
            var boardUsers = await _userService.GetBoardUsers(request.Fk_Board);
            var editorId = await _userService.GetUserIdByToken(request.Token);

            if(!boardUsers.Contains(editorId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CHANGE_VISIBILITY";
                return response;    
            }

            var data = await _repository.ChangeVisibility(request);

            response.Task = TransformDataInDomain(data);
            response.IsSuccessful = true;

            return response;
        }
        catch(Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            response.Task = null;
            return response;
        }
    }

    public async Task<ChangeTaskStageResponseDTO> ChangeStage(ChangeTaskStageRequestDTO request)
    {
        var response = new ChangeTaskStageResponseDTO();   
        try{
            var boardUsers = await _userService.GetBoardUsers(request.Fk_Board);
            var editorId = await _userService.GetUserIdByToken(request.Token);

            if(!boardUsers.Contains(editorId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CHANGE_STAGE";
                return response;    
            }

            var data = await _repository.ChangeStage(request);

            response.Task = TransformDataInDomain(data);
            response.IsSuccessful = true;

            return response;
        }
        catch(Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            response.Task = null;
            return response;
        }
    }

    public async Task<List<Domain.Task>> GetTasksByUser(long userId){
        var response = new List<Domain.Task>();
        var data = await _repository.GetTasksByUser(userId);
        foreach(Models.Database.Task task in data){
            response.Add(TransformDataInDomain(task));
        }
        return response;
    }

    public async Task<bool> HandleTaskOwnerLeavingGroup(long taskId, long boardOwner)
    {
        var data = await _repository.ChangeTaskAssigneeUser(taskId, boardOwner);
        if(data.Fk_UserAssignee != boardOwner){
            return false;
        }
        return true;
    }

    public async Task<ChangeTaskAssigneeResponseDTO> ChangeAssignee(ChangeTaskAssigneeRequestDTO request)
    {
        var response = new ChangeTaskAssigneeResponseDTO();
        try{
            var boardUsers = await _userService.GetBoardUsers(request.Fk_Board);
            var editorId = await _userService.GetUserIdByToken(request.Token);
            var task = await _repository.RetrieveTask(request.Id);

            if(!boardUsers.Contains(request.Assignee)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_BE_ASSIGNEE";
                return response;
            }

            if(task.Fk_UserAssignee != null && !(await _userService.GetBoardOwner(request.Fk_Board) == editorId || task.Fk_UserAssignee == editorId)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CHANGE_ASSIGNEE";
                return response;    
            }

            var data = await _repository.ChangeTaskAssigneeUser(request.Id, request.Assignee);

            response.Task = TransformDataInDomain(data);
            response.IsSuccessful = true;

            return response;
        }
        catch(Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            response.Task = null;
            return response;
        }
    }

        private Domain.Task TransformDataInDomain(Models.Database.Task data){
        var task = new Domain.Task();
        
        task.Id = data.Id;
        task.Title = data.Title;
        task.Description = data.Description;
        task.Fk_Board = data.Fk_Board;
        task.Fk_UserAssignee = data.Fk_UserAssignee;
        task.StageNumber = data.StageNumber;
        task.Hidden = data.Hidden;

        return task;
    }

}