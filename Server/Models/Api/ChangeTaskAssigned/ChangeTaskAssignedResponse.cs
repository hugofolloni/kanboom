namespace Kanboom.Models.ChangeTaskAssigned;

public class ChangeTaskAssignedResponse : BaseResponse
{
    public Domain.Task? Task { get; set; }
    public static ChangeTaskAssignedResponse FromSuccess(Domain.Task task) {
        return new ChangeTaskAssignedResponse {
            Task = task,
            Success = true,
            Message = "CHANGE_TASK_STAGE",
            Exception = null,
            Errors = null
        };
    }
    
    public static ChangeTaskAssignedResponse FromFailure(string Message, List<string>? errors = null) {
        return new ChangeTaskAssignedResponse {
            Task = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static ChangeTaskAssignedResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new ChangeTaskAssignedResponse {
            Task = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}