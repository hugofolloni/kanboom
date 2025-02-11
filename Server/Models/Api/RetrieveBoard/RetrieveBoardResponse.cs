namespace Kanboom.Models.RetrieveBoard;

public class RetrieveBoardResponse : BaseResponse
{
    public Domain.Board? Board { get; set; }
    public static RetrieveBoardResponse FromSuccess(Domain.Board board) {
        return new RetrieveBoardResponse {
            Board = board,
            Success = true,
            Message = "RETRIEVE_BOARD",
            Exception = null,
            Errors = null
        };
    }
    
    public static RetrieveBoardResponse FromFailure(string Message, List<string>? errors = null) {
        return new RetrieveBoardResponse {
            Board = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static RetrieveBoardResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new RetrieveBoardResponse {
            Board = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}