namespace Kanboom.Models.GetUserBoards;

public class GetUserBoardsResponse : BaseRequest
{
    public List<Domain.Board>? boards { get; set; }

}