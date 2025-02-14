using Kanboom.Models.ChangeTaskStage.DTO;
using Kanboom.Models.ChangeTaskVisibility.DTO;
using Kanboom.Models.CreateTask.DTO;
using Kanboom.Models.EditTask.DTO;

namespace Kanboom.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<List<Models.Database.Task>> GetTasksByBoard(long boardId);
    Task<Models.Database.Task> CreateTask(CreateTaskRequestDTO request);
    Task<Models.Database.Task> EditTask(EditTaskRequestDTO request);
    Task<Models.Database.Task> ChangeVisibility(ChangeTaskVisibilityRequestDTO request);
    Task<Models.Database.Task> ChangeStage(ChangeTaskStageRequestDTO request);
    Task<List<Models.Database.Task>> GetTasksByUser(long userId);
    Task<Models.Database.Task> ChangeTaskAssignedUser(long taskId, long userId);
    Task<Models.Database.Task> RetrieveTask(long taskId);
}