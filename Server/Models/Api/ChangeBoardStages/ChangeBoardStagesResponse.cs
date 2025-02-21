namespace Kanboom.Models.ChangeBoardStages;

public class ChangeBoardStagesResponse : BaseResponse
{
    public Domain.Board? Board { get; set; }
    public static ChangeBoardStagesResponse FromSuccess(Domain.Board board) {
        return new ChangeBoardStagesResponse {
            Board = board,
            Success = true,
            Message = "CHANGE_BOARD_STAGES",
            Exception = null,
            Errors = null
        };
    }
    
    public static ChangeBoardStagesResponse FromFailure(string Message, List<string>? errors = null) {
        return new ChangeBoardStagesResponse {
            Board = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static ChangeBoardStagesResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new ChangeBoardStagesResponse {
            Board = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 
}