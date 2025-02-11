namespace Kanboom.Models.CreateTask;

public class CreateTaskResponse : BaseResponse
{
    public Domain.Task? Task { get; set; }
    public static CreateTaskResponse FromSuccess(Domain.Task task) {
        return new CreateTaskResponse {
            Task = task,
            Success = true,
            Message = "CREATE_BOARD",
            Exception = null,
            Errors = null
        };
    }
    
    public static CreateTaskResponse FromFailure(string Message, List<string>? errors = null) {
        return new CreateTaskResponse {
            Task = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static CreateTaskResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new CreateTaskResponse {
            Task = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}