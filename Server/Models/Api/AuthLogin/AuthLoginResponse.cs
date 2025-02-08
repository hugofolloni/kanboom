namespace Kanboom.Models.AuthLogin;

public class AuthLoginResponse : BaseResponse
{
    public string? Token { get; set; }

    public static AuthLoginResponse FromSuccess(string token) {
        return new AuthLoginResponse {
            Token = token,
            Success = true,
            Message = "GET_TOKEN",
            Exception = null,
            Errors = null
        };
    }
    
    public static AuthLoginResponse FromFailure(string Message, List<string>? errors = null) {
        return new AuthLoginResponse {
            Token = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static AuthLoginResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new AuthLoginResponse {
            Token = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 

}