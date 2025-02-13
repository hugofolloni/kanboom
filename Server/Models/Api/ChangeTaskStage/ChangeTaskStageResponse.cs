namespace Kanboom.Models.ChangeTaskStage;

public class ChangeTaskStageResponse : BaseResponse
{
    public Domain.Task? Task { get; set; }
    public static ChangeTaskStageResponse FromSuccess(Domain.Task task) {
        return new ChangeTaskStageResponse {
            Task = task,
            Success = true,
            Message = "CHANGE_TASK_STAGE",
            Exception = null,
            Errors = null
        };
    }
    
    public static ChangeTaskStageResponse FromFailure(string Message, List<string>? errors = null) {
        return new ChangeTaskStageResponse {
            Task = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static ChangeTaskStageResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new ChangeTaskStageResponse {
            Task = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}