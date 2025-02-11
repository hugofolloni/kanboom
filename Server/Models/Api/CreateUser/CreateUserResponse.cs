namespace Kanboom.Models.CreateUser;

public class CreateUserResponse : BaseResponse
{
    public string? Username { get; set; }
    public string? Token { get; set; }

    public static CreateUserResponse FromSuccess(string username, string token) {
        return new CreateUserResponse {
            Username = username,
            Token = token,
            Success = true,
            Message = "GET_TOKEN",
            Exception = null,
            Errors = null
        };
    }
    
    public static CreateUserResponse FromFailure(string Message, List<string>? errors = null) {
        return new CreateUserResponse {
            Username = null,
            Token = null,    
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static CreateUserResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new CreateUserResponse {
            Username = null,
            Token = null,    
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 

}