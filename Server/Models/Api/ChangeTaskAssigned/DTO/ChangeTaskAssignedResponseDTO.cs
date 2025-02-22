namespace Kanboom.Models.ChangeTaskAssignee.DTO;

public class ChangeTaskAssigneeResponseDTO
{
    public Domain.Task? Task { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}