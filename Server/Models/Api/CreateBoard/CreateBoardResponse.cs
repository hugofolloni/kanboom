namespace Kanboom.Models.CreateBoard;

public class CreateBoardResponse : BaseResponse
{
    public Domain.Board? Board { get; set; }
    public static CreateBoardResponse FromSuccess(Domain.Board board) {
        return new CreateBoardResponse {
            Board = board,
            Success = true,
            Message = "CREATE_BOARD",
            Exception = null,
            Errors = null
        };
    }
    
    public static CreateBoardResponse FromFailure(string Message, List<string>? errors = null) {
        return new CreateBoardResponse {
            Board = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static CreateBoardResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new CreateBoardResponse {
            Board = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}