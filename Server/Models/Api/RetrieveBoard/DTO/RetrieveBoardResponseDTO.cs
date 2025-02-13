namespace Kanboom.Models.RetrieveBoard.DTO;

public class RetrieveBoardResponseDTO
{
    public Domain.Board? Board { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set;}
}