using Kanboom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kanboom.Utils; 
using Kanboom.Models.CreateTask;
using Kanboom.Models.CreateTask.DTO;
using Kanboom.Models.EditTask;
using Kanboom.Models.EditTask.DTO;
using Kanboom.Models.ChangeTaskVisibility;
using Kanboom.Models.ChangeTaskVisibilityRequestDTO.DTO;
using Kanboom.Models.ChangeTaskStage;
using Kanboom.Models.ChangeTaskStageRequestDTO.DTO;

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

    [Verification]
    [HttpPatch("task/edit")]
    public async Task<ActionResult<EditTaskResponse>> Edit([FromBody] EditTaskRequest request){
        try {

            var task = new EditTaskRequestDTO{
                Title = request.Title,
                Description = request.Description,
                Fk_UserAssigned = request.Fk_UserAssigned,
                Id = request.Id,
                Token = request.Token,
                Fk_Board = request.Fk_Board
            };


            var response = await _taskService.EditTask(task);

            if(!response.IsSuccessful){
                return BadRequest(EditTaskResponse.FromFailure(response.Message));
            }

            return Ok(EditTaskResponse.FromSuccess(response.Task));

                
        }
        catch(Exception e){
            return StatusCode(500, EditTaskResponse.FromError(e.Message));
        }
    }

    [Verification]
    [HttpPatch("task/changeVisibility")]
    public async Task<ActionResult<ChangeTaskVisibilityResponse>> ChangeVisibility([FromBody] ChangeTaskVisibilityRequest request){
        try {

            var task = new ChangeTaskVisibilityRequestDTO{
                Id = request.Id,
                Token = request.Token,
                Fk_Board = request.Fk_Board,
                Hidden = request.Hidden
            };


            var response = await _taskService.ChangeVisibility(task);

            if(!response.IsSuccessful){
                return BadRequest(ChangeTaskVisibilityResponse.FromFailure(response.Message));
            }

            return Ok(ChangeTaskVisibilityResponse.FromSuccess(response.Task));

                
        }
        catch(Exception e){
            return StatusCode(500, ChangeTaskVisibilityResponse.FromError(e.Message));
        }
    }

    [Verification]
    [HttpPatch("task/changeStage")]
    public async Task<ActionResult<ChangeTaskStageResponse>> ChangeStage([FromBody] ChangeTaskStageRequest request){
        try {

            var task = new ChangeTaskStageRequestDTO{
                Id = request.Id,
                Token = request.Token,
                Fk_Board = request.Fk_Board,
                Stage = request.Stage
            };


            var response = await _taskService.ChangeStage(task);

            if(!response.IsSuccessful){
                return BadRequest(ChangeTaskStageResponse.FromFailure(response.Message));
            }

            return Ok(ChangeTaskStageResponse.FromSuccess(response.Task));

                
        }
        catch(Exception e){
            return StatusCode(500, ChangeTaskStageResponse.FromError(e.Message));
        }
    }
}
