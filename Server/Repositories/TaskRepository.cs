using Kanboom.Repositories.Interfaces;
using Kanboom.Context;
using Microsoft.EntityFrameworkCore;
using Kanboom.Models.CreateTask.DTO;
namespace Kanboom.Repositories;

public class TaskRepository : ITaskRepository {
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context){
        _context = context;
    }
    public async Task<List<Models.Database.Task>> GetTasksByBoard(long boardId){
         return await _context.Task
        .Where(t => t.Fk_Board == boardId)
        .Join(_context.Board,
            task => task.Fk_Board,
            board => board.Id,
            (task, board) => new Models.Database.Task
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description, // Adjust based on your Task entity
                StageNumber = task.StageNumber,
                Fk_UserAssigned = task.Fk_UserAssigned,
                Fk_Board = task.Fk_Board
            })
        .ToListAsync();
    }

    public async Task<Models.Database.Task> CreateTask(CreateTaskRequestDTO request)
    {
        try
        {
            var task = new Models.Database.Task
            {
                Title = request.Title,
                Description = request.Description,
                Fk_Board = request.Fk_Board,
                Fk_UserAssigned = request.Fk_UserAssigned,
                StageNumber = 0,
            };

            _context.Task.Add(task);
            await _context.SaveChangesAsync();
            
            return task;
        }
        catch
        {
            return null;
        }
    }

}