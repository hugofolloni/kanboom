using Kanboom.Models;
using Kanboom.Models.CreateTask.DTO;

namespace Kanboom.Services.Interfaces;

public interface ITaskService {
    Task<int> GetIncompletedTaskNumberByBoard(Models.Database.Board board);
    Task<List<Domain.Task>> GetTasksInBoard(long boardId);
    Task<CreateTaskResponseDTO> CreateTask(CreateTaskRequestDTO request);
}
