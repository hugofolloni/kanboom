namespace Kanboom.Models.ChangeTaskAssigned.DTO;

public class ChangeTaskAssignedResponseDTO
{
    public Domain.Task? Task { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}