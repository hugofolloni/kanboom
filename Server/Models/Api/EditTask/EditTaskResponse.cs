namespace Kanboom.Models.EditTask;

public class EditTaskResponse : BaseResponse
{
    public Domain.Task? Task { get; set; }
    public static EditTaskResponse FromSuccess(Domain.Task task) {
        return new EditTaskResponse {
            Task = task,
            Success = true,
            Message = "EDIT_TASK",
            Exception = null,
            Errors = null
        };
    }
    
    public static EditTaskResponse FromFailure(string Message, List<string>? errors = null) {
        return new EditTaskResponse {
            Task = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static EditTaskResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new EditTaskResponse {
            Task = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}