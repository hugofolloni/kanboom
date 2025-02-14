namespace Kanboom.Models.ChangeBoardOwner;

public class ChangeBoardOwnerResponse : BaseResponse
{
    public Domain.Board? Board { get; set; }
    public static ChangeBoardOwnerResponse FromSuccess(Domain.Board board) {
        return new ChangeBoardOwnerResponse {
            Board = board,
            Success = true,
            Message = "CHANGE_BOARD_OWNER",
            Exception = null,
            Errors = null
        };
    }
    
    public static ChangeBoardOwnerResponse FromFailure(string Message, List<string>? errors = null) {
        return new ChangeBoardOwnerResponse {
            Board = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static ChangeBoardOwnerResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new ChangeBoardOwnerResponse {
            Board = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}