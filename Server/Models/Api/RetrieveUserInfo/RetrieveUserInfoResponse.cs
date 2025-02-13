namespace Kanboom.Models.RetrieveUserInfo;

public class RetrieveUserInfoResponse : BaseResponse
{
    public string? Username { get; set; }
    public string? ProfilePic { get; set; }
    public List<Domain.Board>? Boards { get; set; }
    public List<Domain.Task>? Tasks { get; set; }
    public long? Id { get; set; }

    public static RetrieveUserInfoResponse FromSuccess(string username, string profilePic, List<Domain.Board> boards, List<Domain.Task> tasks, long? id) {
        return new RetrieveUserInfoResponse {
            Username = username,
            ProfilePic = profilePic,
            Boards = boards,
            Tasks = tasks,
            Id = id,
            Success = true,
            Message = "GET_USER_INFO",
            Exception = null,
            Errors = null
        };
    }
    
    public static RetrieveUserInfoResponse FromFailure(string Message, List<string>? errors = null) {
        return new RetrieveUserInfoResponse {
            Username = null,
            ProfilePic = null,
            Boards = null,
            Tasks = null,
            Id = null,
            Success = false,
            Message = Message,
            Exception = null,
            Errors = errors
        };
    } 

    public static RetrieveUserInfoResponse FromError(string ExceptionMessage, List<string>? errors = null) {
        return new RetrieveUserInfoResponse {
            Username = null,
            ProfilePic = null,
            Boards = null,
            Tasks = null,
            Id = null,
            Success = false,
            Message = "ERROR",
            Exception = ExceptionMessage,
            Errors = errors
        };
    } 

}