namespace Kanboom.Models.ChangeTaskAssigned.DTO;

public class ChangeTaskAssignedRequestDTO 
{
    public long Id { get; set; }
    public required long Assigned { get; set; }
    public string? Token { get; set; }
    public long? Fk_Board { get; set; }

}