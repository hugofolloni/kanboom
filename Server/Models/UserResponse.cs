using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models;

public class UserResponse : BaseResponse
{
    public string? Token { get; set; }

    public static UserResponse FromSuccess(string token) {
        return new UserResponse {
            Token = token,
            Success = true,
            Message = "GET_TOKEN",
            Exception = null,
            Errors = null
        };
    }
    
    public static UserResponse FromFailure(String Message, List<string>? errors = null) {
        return new UserResponse {
            Token = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static UserResponse FromError(String ExceptionMessage, List<string>? errors = null) {
        return new UserResponse {
            Token = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 

}