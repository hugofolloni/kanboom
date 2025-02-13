using Kanboom.Models.ChangeTaskStageRequestDTO.DTO;
using Kanboom.Models.ChangeTaskVisibilityRequestDTO.DTO;
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
}