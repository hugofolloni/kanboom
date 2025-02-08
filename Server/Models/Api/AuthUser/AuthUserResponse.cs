using System.ComponentModel.DataAnnotations;
using Kanboom.Models;
namespace Kanboom.Models.AuthUser;

public class AuthUserResponse : BaseResponse
{
    public string? Token { get; set; }

    public static AuthUserResponse FromSuccess(string token) {
        return new AuthUserResponse {
            Token = token,
            Success = true,
            Message = "GET_TOKEN",
            Exception = null,
            Errors = null
        };
    }
    
    public static AuthUserResponse FromFailure(String Message, List<string>? errors = null) {
        return new AuthUserResponse {
            Token = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static AuthUserResponse FromError(String ExceptionMessage, List<string>? errors = null) {
        return new AuthUserResponse {
            Token = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 

}