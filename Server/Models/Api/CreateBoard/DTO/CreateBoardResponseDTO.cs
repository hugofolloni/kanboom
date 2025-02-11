namespace Kanboom.Models.CreateBoard.DTO;

public class CreateBoardResponseDTO
{
    public Domain.Board? Board { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set;}
}