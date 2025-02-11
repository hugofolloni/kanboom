using Kanboom.Models.CreateTask.DTO;

namespace Kanboom.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<List<Models.Database.Task>> GetTasksByBoard(long boardId);
    Task<Models.Database.Task> CreateTask(CreateTaskRequestDTO request);
}