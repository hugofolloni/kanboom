namespace Kanboom.Models.RetrieveBoard.DTO;

public class RetrieveBoardReponseDTO
{
    public Domain.Board? Board { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set;}
}