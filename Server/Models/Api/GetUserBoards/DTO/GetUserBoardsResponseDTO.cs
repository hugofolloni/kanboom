namespace Kanboom.Models.GetUserBoards.DTO;

public class GetUserBoardsResponseDTO
{
    public List<Domain.Board>? boards { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}