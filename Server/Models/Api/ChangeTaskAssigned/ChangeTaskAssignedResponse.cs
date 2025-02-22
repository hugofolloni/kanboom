namespace Kanboom.Models.ChangeTaskAssignee;

public class ChangeTaskAssigneeResponse : BaseResponse
{
    public Domain.Task? Task { get; set; }
    public static ChangeTaskAssigneeResponse FromSuccess(Domain.Task task) {
        return new ChangeTaskAssigneeResponse {
            Task = task,
            Success = true,
            Message = "CHANGE_TASK_STAGE",
            Exception = null,
            Errors = null
        };
    }
    
    public static ChangeTaskAssigneeResponse FromFailure(string Message, List<string>? errors = null) {
        return new ChangeTaskAssigneeResponse {
            Task = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static ChangeTaskAssigneeResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new ChangeTaskAssigneeResponse {
            Task = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}