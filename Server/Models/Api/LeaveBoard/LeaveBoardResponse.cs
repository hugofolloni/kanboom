using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.LeaveBoard;

public class LeaveBoardResponse : BaseResponse
{
    public static LeaveBoardResponse FromSuccess() {
        return new LeaveBoardResponse {
            Success = true,
            Message = "LEAVE_BOARD",
            Exception = null,
            Errors = null
        };
    }
    
    public static LeaveBoardResponse FromFailure(string Message, List<string>? errors = null) {
        return new LeaveBoardResponse {
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static LeaveBoardResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new LeaveBoardResponse {
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}