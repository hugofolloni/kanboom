namespace Kanboom.Models.ChangeTaskVisibility;

public class ChangeTaskVisibilityResponse : BaseResponse
{
    public Domain.Task? Task { get; set; }
    public static ChangeTaskVisibilityResponse FromSuccess(Domain.Task task) {
        return new ChangeTaskVisibilityResponse {
            Task = task,
            Success = true,
            Message = "CHANGE_TASK_VISIBILITY",
            Exception = null,
            Errors = null
        };
    }
    
    public static ChangeTaskVisibilityResponse FromFailure(string Message, List<string>? errors = null) {
        return new ChangeTaskVisibilityResponse {
            Task = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static ChangeTaskVisibilityResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new ChangeTaskVisibilityResponse {
            Task = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}