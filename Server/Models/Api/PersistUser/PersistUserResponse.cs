namespace Kanboom.Models.PersistUser;

public class PersistUserResponse : BaseResponse
{
    public string? Username { get; set; }

    public static PersistUserResponse FromSuccess(string username) {
        return new PersistUserResponse {
            Username = username,
            Success = true,
            Message = "PERSIST_USER",
            Exception = null,
            Errors = null
        };
    }
    
    public static PersistUserResponse FromFailure(string Message, List<string>? errors = null) {
        return new PersistUserResponse {
            Username = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static PersistUserResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new PersistUserResponse {
            Username = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 

}