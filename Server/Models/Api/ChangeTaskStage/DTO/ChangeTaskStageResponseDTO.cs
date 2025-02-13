namespace Kanboom.Models.ChangeTaskStageRequestDTO.DTO;

public class ChangeTaskStageResponseDTO
{
    public Domain.Task? Task { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}