namespace Kanboom.Models.ChangeTaskVisibility.DTO;

public class ChangeTaskVisibilityResponseDTO
{
    public Domain.Task? Task { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}