namespace Kanboom.Models.ChangeBoardStages.DTO;

public class ChangeBoardStagesResponseDTO
{
    public Domain.Board? Board { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}