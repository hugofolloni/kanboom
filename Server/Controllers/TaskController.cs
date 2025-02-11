using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Utils; 
using Kanboom.Models.CreateTask;
using Kanboom.Models.CreateTask.DTO;

namespace Kanboom.Controllers;

public class TaskController : ControllerBase 
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService){
        _taskService = taskService;
    }
    
    [Verification]
    [HttpPost("task/create")]
    public async Task<ActionResult<CreateTaskResponse>> Create([FromBody] CreateTaskRequest request){
        try {
            var task = new CreateTaskRequestDTO{
                Title = request.Title,
                Description = request.Description,
                Fk_UserAssigned = request.Fk_UserAssigned,
                Token = request.Token,
                Fk_Board = request.Fk_Board
            };

            var response = await _taskService.CreateTask(task);

            if(!response.IsSuccessful){
                return BadRequest(CreateTaskResponse.FromFailure(response.Message));
            }

            return Ok(CreateTaskResponse.FromSuccess(response.Task));

                
        }
        catch(Exception e){
            return StatusCode(500, CreateTaskResponse.FromError(e.Message));
        }
    }

}
