namespace Kanboom.Models.ChangeBoardOwner.DTO;

public class ChangeBoardOwnerResponseDTO
{
    public Domain.Board? Board { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}