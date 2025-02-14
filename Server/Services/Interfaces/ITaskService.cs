using Kanboom.Models;
using Kanboom.Models.ChangeTaskAssigned.DTO;
using Kanboom.Models.ChangeTaskStage.DTO;
using Kanboom.Models.ChangeTaskVisibility.DTO;
using Kanboom.Models.CreateTask.DTO;
using Kanboom.Models.EditTask.DTO;

namespace Kanboom.Services.Interfaces;

public interface ITaskService {
    Task<int> GetIncompletedTaskNumberByBoard(Models.Database.Board board);
    Task<List<Domain.Task>> GetTasksInBoard(long boardId);
    Task<CreateTaskResponseDTO> CreateTask(CreateTaskRequestDTO request);
    Task<EditTaskResponseDTO> EditTask(EditTaskRequestDTO request);
    Task<ChangeTaskVisibilityResponseDTO> ChangeVisibility(ChangeTaskVisibilityRequestDTO request);
    Task<ChangeTaskStageResponseDTO> ChangeStage(ChangeTaskStageRequestDTO request);
    Task<List<Domain.Task>> GetTasksByUser(long userId);
    Task<bool> HandleTaskOwnerLeavingGroup(long taskId, long userId);
    Task<ChangeTaskAssignedResponseDTO> ChangeAssigned(ChangeTaskAssignedRequestDTO request);
}