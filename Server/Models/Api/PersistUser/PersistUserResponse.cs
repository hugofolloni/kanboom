namespace Kanboom.Models.PersistUser;

public class PersistUserResponse : BaseResponse
{
    public string? Username { get; set; }

    public static PersistUserResponse FromSuccess(string username) {
        return new PersistUserResponse {
            Username = username,
            Success = true,
            Message = "GET_TOKEN",
            Exception = null,
            Errors = null
        };
    }
    
    public static PersistUserResponse FromFailure(String Message, List<string>? errors = null) {
        return new PersistUserResponse {
            Username = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static PersistUserResponse FromError(String ExceptionMessage, List<string>? errors = null) {
        return new PersistUserResponse {
            Username = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 

}