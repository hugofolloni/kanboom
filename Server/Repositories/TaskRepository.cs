using Kanboom.Repositories.Interfaces;
using Kanboom.Context;
using Microsoft.EntityFrameworkCore;
using Kanboom.Models.CreateTask.DTO;
using Kanboom.Models.EditTask.DTO;
using Kanboom.Models;
using Kanboom.Models.ChangeTaskVisibility.DTO;
using Kanboom.Models.ChangeTaskStage.DTO;
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
                Fk_UserAssignee = task.Fk_UserAssignee,
                Fk_Board = task.Fk_Board,
                Hidden = task.Hidden
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
                Fk_UserAssignee = request.Fk_UserAssignee,
                StageNumber = 0,
                Hidden = false
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

    public async Task<Models.Database.Task> EditTask(EditTaskRequestDTO request)
    {
        try
        {
            var task = await _context.Task.FirstOrDefaultAsync(t => t.Id == request.Id);

            if(task == null){
                throw new Exception("TASK_NOT_FOUND");
            }

            if(task.Hidden){
                throw new Exception("TASK_COULDNT_BE_EDITED");
            }

            if(task.Fk_Board != request.Fk_Board){
                throw new Exception("TASK_NOT_IN_BOARD");
            }

            if(!string.IsNullOrEmpty(request.Title)){
                task.Title = request.Title;
            }

            if(!string.IsNullOrEmpty(request.Description)){
                task.Description = request.Description;
            }

            if(request.Fk_UserAssignee != null){
                task.Fk_UserAssignee = request.Fk_UserAssignee;
            }

            _context.Task.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Models.Database.Task> ChangeVisibility(ChangeTaskVisibilityRequestDTO request){
        try
        {
            var task = await _context.Task.FirstOrDefaultAsync(t => t.Id == request.Id);

            if(task == null){
                throw new Exception("TASK_NOT_FOUND");
            }

            if(task.Fk_Board != request.Fk_Board){
                throw new Exception("TASK_NOT_IN_BOARD");
            }

            task.Hidden = request.Hidden;

            _context.Task.Update(task);
            await _context.SaveChangesAsync();

            return task;    
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Models.Database.Task> ChangeStage(ChangeTaskStageRequestDTO request)
    {
        try
        {
            var task = await _context.Task.FirstOrDefaultAsync(t => t.Id == request.Id);

            if(task == null){
                throw new Exception("TASK_NOT_FOUND");
            }

            if(task.Fk_Board != request.Fk_Board){
                throw new Exception("TASK_NOT_IN_BOARD");
            }

            var stages = await _context.StageLevels.Where(s => s.Fk_Board == request.Fk_Board).ToListAsync();
            var maxStage = stages.Max(s => s.StageNumber);

            if(request.Stage < 0 || request.Stage > maxStage){
                throw new Exception("INVALID_STAGE");
            }

            task.StageNumber = request.Stage;

            _context.Task.Update(task);
            await _context.SaveChangesAsync();

            return task;    
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Models.Database.Task>> GetTasksByUser(long userId){
        return await _context.Task.Where(t => t.Fk_UserAssignee == userId).ToListAsync();
    }

    public async Task<Models.Database.Task> ChangeTaskAssigneeUser(long taskId, long userId)
    {
        try
        {
            var task = await _context.Task.FirstOrDefaultAsync(t => t.Id == taskId);

            if(task == null){
                throw new Exception("TASK_NOT_FOUND");
            }

            task.Fk_UserAssignee = userId;

            _context.Task.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Models.Database.Task> RetrieveTask(long taskId)
    {
        return await _context.Task.FirstOrDefaultAsync(t => t.Id == taskId);
    }
}