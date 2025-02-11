using Kanboom.Models.Database;
using Kanboom.Services.Interfaces;
using Kanboom.Repositories.Interfaces;
using Kanboom.Models;
using Kanboom.Models.CreateTask.DTO;

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
            var boardTask = new Domain.Task{Title = task.Title, Description = task.Description, StageNumber = task.StageNumber, Fk_UserAssigned = task.Fk_UserAssigned, Fk_Board = task.Fk_Board};
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

            if(!boardUsers.Contains(creatorId) || !boardUsers.Contains(request.Fk_UserAssigned)){
                response.IsSuccessful = false;
                response.Message = "USER_CANT_CREATE_OR_BE_ASSIGNED_TO_TASK";
                return response;    
            }

            var data = await _repository.CreateTask(request);

            var task = new Domain.Task();
            
            task.Title = data.Title;
            task.Description = data.Description;
            task.Fk_Board = data.Fk_Board;
            task.Fk_UserAssigned = data.Fk_UserAssigned;
            task.StageNumber = data.StageNumber;

            response.Task = task;
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
}